//classe que vai trabalhar com o banco
SQLManager.$inject = ['database', 'results'];
function SQLManager(database,results) {
    
    //public methods
    this.execute = function(cmd) {

        database.transaction(function(tr) {
            tr.executeSql(cmd, [], successCallback, errorCallback);
        });
        

    };


    //private methods
    function successCallback(tr,data) {
        var length = data.rows.length;

        results.clear();

        for (var i = 0; i < length; i++) {
                       
            var item = data.rows.item(i);
                results.push(item);
        }


    }
    function errorCallback(tr,error) {
        toastr.error(error.message, 'Code ' + error.code);
        results.clear();
    }

}