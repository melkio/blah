var express = require('express');
var app = express();
var amqp = require('amqplib');
var when = require('when');
var mongodb = require('mongodb').MongoClient

app.set('view engine', 'pug');

app.get('/', function (req, res) {
  res.send('Hello World!');
});

app.get('/messages', function(req, res){
  mongodb.connect('mongodb://database/dotnetlombardia', function(err, db) {
    db.collection('messages').find({}).toArray(function(err, docs){
      res.render('messages', {docs: docs})
    })
  })
})

app.post('/messages', (req, res) => {
  amqp.connect('amqp://broker').then(function(conn) {
    return when(conn.createChannel().then(function(ch) {
      var q = 'task_queue';
      var msg = 'Hello World!';

      var ok = ch.assertQueue(q, {durable: true});

      return ok.then(function(_qok) {
        ch.sendToQueue(q, new Buffer(msg));
        console.log(" [x] Sent '%s'", msg);
        return ch.close();
      });
    })).ensure(function() { conn.close(); res.json({status: 'ok', text: 'message sent'}) });
  }).then(null, console.warn);
})

app.listen(3000, function () {
  console.log('Example app listening on port 3000!');
});