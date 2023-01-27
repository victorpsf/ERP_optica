const { Sleep } = require('../Lib/index')
const { Guid } = require('../Lib/Util')


const RequestManager = require('./RequestManager')
const readerConfig = require('../Lib/ReaderConfig')
const cache = require('../Lib/Cache');

const gateway = {}
const appCache = cache.Create('@');

gateway.config = readerConfig;

gateway.AntiDDOS = async function ({ time }) {
  while (true) {
    await Sleep(time);
    const connections = appCache.Get('connections', {});

    for (const host in connections) {
      const { date } = (connections[host] || { count: 0, date: new Date() });
      const startDate = new Date(date.toJSON());
      const currentDate = new Date();
      
      startDate.setSeconds(startDate.getSeconds() + time);
      if (currentDate >= startDate) {
        delete connections[host];
      }
    }
  }
}

gateway.KeyCache = async function ({ Authorization }, host) {
  let next = true;

  if (Authorization) {

  }

  else {

  }

  return next;
}

gateway.Middleware = async function ({ req, res, next }) {
  req.guid = Guid();
  var manager = RequestManager.Create(req, res);
  const { data, headers, host, port, url, method, info } = manager.Extract();
  const connectionInfo = appCache.Get(`connections@${host}`, { count: 0, date: new Date(), Authorization: null });

  // se realizou o maximo de chamadas ao servidor no periodo de 1 minuto
  if (connectionInfo.count >= this.config.GetServerConfig('max_connections', 60))
    return manager.Response.TooManyRequests();

  else {
    connectionInfo.count += 1;
    appCache.Set(`connections@${host}`, connectionInfo);
  }

  // se tiver path de serviço
  if (!info.Exists)
    return manager.Response.Ok();

  // verifica se o token foi salvo a primeira vez pelo host atual
  // caso não não permite requisição e coloca na black-list
  if (!this.KeyCache(headers, host))
    return manager.Response.Unauthorized();

  if (!this.config.ServiceExists(info.Model.prefix))
    return manager.Response.DontFound();

  var urlService = this.config.GetService(info.Model.prefix);
  return manager.CallCluster(urlService.GetUrl({ controller: info.Model.controller, action: info.Model.action }), { data, headers, method })
}

module.exports = gateway;