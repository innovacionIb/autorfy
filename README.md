# hyperledger fabric based project and unity

## Pre Requisites
### Installing cURL
Open a terminal window: CTRL+ALT+T.
Type the following command and enter your password:
$ sudo apt install curl <br/>
To check, run the following command in your terminal/command line:<br/>
$ curl -V <br/>
Note: The "V" is capitalized.<br/>
### Installing Docker
Docker provides great instructions on how to install it [here](https://docs.docker.com/install/linux/docker-ce/ubuntu/).<br/>
### Installing Node.js and npm
To install Node.js and npm, run the following commands in your terminal/command line:<br/>
$ sudo bash -c "cat >/etc/apt/sources.list.d/nodesource.list" <<EOL<br/>
deb https://deb.nodesource.com/node_6.x xenial main<br/>
deb-src https://deb.nodesource.com/node_6.x xenial main<br/>
EOL<br/>
$ curl -s https://deb.nodesource.com/gpgkey/nodesource.gpg.key | sudo apt-key add -<br/>
$ sudo apt update<br/>
$ sudo apt install nodejs<br/>
$ sudo apt install npm<br/>
Verify the installation, as well as  the versions of both Node.js and npm, and make sure the Node.js version you are installing is greater than v6.9 (do not use v7), and the npm version is greater than 3.x:<br/>
$ node --version && npm --version<br/>
### Installing Go Language<br/>
Visit https://golang.org/dl/ and make note of the latest stable release (v1.8 or later).<br/>
To install Go language, run the following commands in your terminal/command line:<br/>
$ sudo apt update<br/>
$ sudo curl -O https://storage.googleapis.com/golang/go1.9.2.linux-amd64.tar.gz <br/>
Note: Switch out the black portion of the URL with the correct filename.<br/>
$ sudo tar -xvf go1.9.2.linux-amd64.tar.gz<br/>
$ sudo mv go /usr/local<br/>
$ echo 'export PATH=$PATH:/usr/local/go/bin' >> ~/.profile<br/>
$ source ~/.profile<br/>
Check that the Go version is v1.8 or later:<br/>
$ go version<br/>

##The project 
###For running blochain 

First, remove any pre-existing containers, as it may conflict with commands in this tutorial:

$ docker rm -f $(docker ps -aq)

go to the Work folder
cd   autorFY/autorfy-app

Then, let’s start the Hyperledger Fabric network with the following command:

$ ./startFabric.sh

Install the required libraries from the package.json file, register the Admin and User components of our network, and start the client application with the following commands:

$ npm install

$ node registerAdmin.js

$ node registerUser.js

$ node server.js

Load the client simply by opening localhost:8000 in any browser window of your choice, and you should see the user interface for our simple application at this URL (as in the screenshot below).

Example of Blockchain here.   http://ibinnovation9734.cloudapp.net:8000 
