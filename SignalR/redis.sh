sudo apt-get update 
sudo apt-get -y install make
mkdir /opt/redis
cd /opt/redis
wget http://download.redis.io/releases/redis-3.2.5.tar.gz
tar xzf redis-3.2.5.tar.gz
cd redis-3.2.5
make 

cp src/redis-server /usr/local/bin
cp src/redis-cli /usr/local/bin

sudo mkdir /etc/redis
sudo mkdir /var/redis
sudo mkdir /var/redis/6379

sudo cp utils/redis_init_script /etc/init.d/redis_6379
sudo cp /vagrant/redis.conf /etc/redis/6379.conf

sudo update-rc.d redis_6379 defaults
sudo /etc/init.d/redis_6379 start




