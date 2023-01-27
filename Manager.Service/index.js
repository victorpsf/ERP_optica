const http = require('http');
const express = require('express');
const process = require('process');
const dotenv = require('dotenv')
const cors = require('cors');
const dataReader = require('./Lib/DataReader')
const readerConfig = require('./Lib/ReaderConfig');
const gateway = require('./Gateway/index')
const config = require('./configuration.json')

dotenv.config();
const app = express();
const server = http.createServer(app);

gateway.config = readerConfig.Configure(config, { max_connections: (parseInt(process.env.MAX_REQUESTS) || 60) });
dataReader(app);
app.use(
  cors({
    origin: 'http://localhost:8080',//`http://localhost:${process.env.SERVER_PORT}`,
    methods: ['GET', 'POST', 'PUT', 'DELETE'],
    allowedHeaders: '*'
  })
)

gateway.AntiDDOS({ time: (parseFloat(process.env.INTERVAL_TIME) || 60) });

app.use((req, res, next) => gateway.Middleware({ req, res, next }));
server.listen(process.env.SERVER_PORT, () => console.log(`server open in http://localhost:${process.env.SERVER_PORT}/`))