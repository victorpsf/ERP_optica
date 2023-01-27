exports.Observer = function (data) {
  const internal = function (value) {
    if (value === undefined)
      return data;
    else 
      data = value;
  }

  return internal;
}

exports.Sleep = (time) => new Promise((resolve) => {
  time = parseFloat(time) || 1;
  time = time * 1000;

  setTimeout(() => resolve(true), time);
})

exports.extractInfoPath = (url) => (/\/api\/([a-zA-Z]{2})\/([a-zA-Z]+)\/?([a-zA-Z]+)?/.exec(url));
exports.isDevelopment = () => (process.env.NODE_ENV != 'production');