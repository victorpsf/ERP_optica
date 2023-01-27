const Axios = require('axios');
const { request, response } = require('express')

const Response = function ({ req = request, res = response }) {
  this.send = async function ({ code, status, type, text, data, headers }) {
    try {
      res.status(code);

      if (status) res.statusMessage = status;
      if (type) res.type(type);
      if (text) res.write(text);
      if (data) res.json(data);
      if (headers) req.headers = headers;

      console.log(`[${req.guid}] RESPONSE [${req.method}] ${req.url} ${(new Date()).toLocaleTimeString()}`);
      res.end();
    }
    catch(error) {
      console.log(`[${req.guid}] ${error}`);
      console.log(`[${req.guid}] RESPONSE ERROR [${req.method}] ${req.url}`);
      this.send({ code: 500, status: "INTERNAL SERVER ERROR" });
    }
  }

  this.TooManyRequests = function () {
    this.send({
      code: 429,
      type: '.html',
      text: `
<div>
  <div>
    <h1>Too Many Requestes</h1>
  </div>
  <div>
    <p>foi realizada muitas chamadas para o servidor, você está temporariamente impossibilitado de realizar novas chamadas</p>
  </div>
  <div>
    <p>Após um periodo de tempo você podera realizar novas chamadas ao servidor.</p>
  </div>
</div>
`
    });
  }

  this.Redirect = function (uri) {
    res.status(301);
    res.redirect(uri);
    res.end();
  }

  this.Ok = function (body) {
    const argument = { code: 200 };
    argument[(typeof body == 'object' ? 'data' : 'text')] = body;
    this.send(argument);
  }

  this.InternalServerError = function () {
    this.send({ code: 500, status: "INTERNAL SERVER ERROR" });
  }

  this.ServiceResponse = function ({ status, statusText, headers, data }) {
    const argument = {
      code: typeof status == 'number' ? status : 500,
      status: statusText,
      headers
    };
    argument[typeof data == 'object' ? 'data' : 'text'] = data
    this.send(argument)
  }

  this.Unauthorized = function () {
    this.send({ code: 401 });
  }

  this.DontFound = function () {
    this.send({ code: 404 });
  }
}

const PathExtract = function (url) {
  this.Url = url;
  this.Info = (/\/api\/([a-zA-Z]{2})\/([a-zA-Z]+)\/?([a-zA-Z]+)?/.exec(url));
  this.Model = {}
  this.Exists = (this.Info) ? true: false;

  if (this.Exists) {
    const [path, prefix, controller, action ] = this.Info;
    this.Model = { path, prefix, controller, action }
  }
}

const RequestManager = function ({req, res}) {
  console.log(`[${req.guid}] REQUEST [${req.method}] ${req.url} ${(new Date()).toLocaleTimeString()}`);
  this.Extract = function () {
    return {
      data: Object.assign({}, req.body, req.query, req.params),
      headers: Object.assign({}, req.headers),
  
      host: req.socket.remoteAddress,
      port: req.socket.remotePort,
  
      url: req.url,
      method: req.method,
      info: new PathExtract(req.url),
    }
  }

  this.CallCluster = async function (url, { data, headers, method }) {
    try {
      const result = await Axios({
        url,
        data,
        method,
        headers,
        timeout: 22000
      });

      this.Response.send({ code: result.status, status: result.statusText, data: result.data, headers: result.headers });
    }

    catch (error) {
      console.log(`[${req.guid}] CLUSTER ERROR: ${url}\n${error}`);
      this.Response.send({ data: null, code: isNaN(parseInt(error.code)) ? 500: error.code, status: error.message? error.message: 'INTERNAL SERVER ERROR', headers: {} });
    }
  }

  this.Response = new Response({ req, res })
}

RequestManager.Create = (req, res) => new RequestManager({ req, res });

module.exports = RequestManager
