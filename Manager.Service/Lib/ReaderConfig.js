const UrlService = require('./UrlService');

class ReaderConfig {
  constructor() {
    this.services = [];
    this.server = {};
  }

  Configure(services, server) {
    this.services = services;
    this.server = server;

    return this;
  }

  ServiceExists(prefix = '') {
    return (this.GetService(prefix)) ? true : false;
  }

  GetService(prefix = '') {
    return UrlService.Create(this.services.filter(a => a.prefix == prefix)[0]) || null;
  }

  GetServerConfig(propertie, defaultValue) {
    return this.server[propertie] || defaultValue;
  }
}

module.exports = new ReaderConfig();