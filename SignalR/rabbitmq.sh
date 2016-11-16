echo 'deb http://www.rabbitmq.com/debian/ testing main' | sudo tee /etc/apt/sources.list.d/rabbitmq.list
wget -O- https://www.rabbitmq.com/rabbitmq-release-signing-key.asc | sudo apt-key add -
sudo apt-get update
sudo apt-get install -y rabbitmq-server

sudo rabbitmq-plugins enable rabbitmq_management
sudo service rabbitmq-server restart

sudo rabbitmqctl add_user admin CodicePlastico
sudo rabbitmqctl set_user_tags admin administrator
sudo rabbitmqctl set_permissions admin ".*" ".*" ".*"
sudo rabbitmqctl delete_user guest