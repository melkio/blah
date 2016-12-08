var amqp = require('amqplib');
var mongodb = require('mongodb').MongoClient

amqp.connect('amqp://broker').then(function(conn) {
  process.once('SIGINT', function() { conn.close(); });
  return conn.createChannel().then(function(ch) {
    var ok = ch.assertQueue('task_queue', {durable: true});
    ok = ok.then(function() { ch.prefetch(1); });
    ok = ok.then(function() {
      ch.consume('task_queue', doWork, {noAck: false});
      console.log(" [*] Waiting for messages. To exit press CTRL+C");
    });
    return ok;

    function doWork(msg) {
      var body = msg.content.toString();
      console.log(" [x] Received '%s'", body);
      mongodb.connect('mongodb://database/dotnetlombardia', function(err, db) {
        db.collection('messages').insert({data: body, timestamp: Date.now()})
        db.close();
        setTimeout(function(){
          console.log("Connected successfully to server");
          ch.ack(msg);
        }, 1000)
      });
    }
  });
}).then(null, console.warn);