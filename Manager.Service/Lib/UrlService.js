const UrlService = function ({ protocol, domain, port, name, prefix }) {
  this.GetUrl = function ({ controller, action }) {
    return `${protocol}://${domain}:${port}/${controller}/${action}`;
  }
}

UrlService.Create = function ({ protocol, domain, port, name, prefix }) {
  return new UrlService({ protocol, domain, port, name, prefix });
};

module.exports = UrlService;