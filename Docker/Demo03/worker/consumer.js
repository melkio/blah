var amqp = require('amqplib');
var mongodb = require('mongodb').MongoClient

function run(conn) {
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
          var date = new Date()
          var hours = date.getHours() < 10 ? "0" + date.getHours().toString() : date.getHours().toString()
          var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes().toString() : date.getMinutes().toString()
          var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds().toString() : date.getSeconds().toString()
          db.collection('messages').update({_id: `${hours}-${minutes}-${seconds}`}, {$inc: {counter:1 }}, {upsert:true})
          db.close();
          setTimeout(function(){
            console.log("Connected successfully to server");
            ch.ack(msg);
          }, 1000)
        });
      }
    });
}

function connect() {
  amqp.connect('amqp://broker')
    .then((conn) => run(conn))
    .catch(() => {
      console.log("Error during connection. Try again later...")
      setTimeout(connect, 1000)
    })
}

connect()