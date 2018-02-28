//SPDX-License-Identifier: Apache-2.0

var tuna = require('./controller.js');

module.exports = function(app){

  app.get('/get_obra/:id', function(req, res){
    tuna.get_obra(req, res);
  });
  app.get('/add_obra/:obra', function(req, res){
    tuna.add_obra(req, res);
  });
  app.get('/get_all_obra', function(req, res){
    tuna.get_all_obra(req, res);
  });

}
