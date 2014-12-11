(function (ng) {

    var dbInfo = {
        name: 'websql',
        version: '1.0',
        displayName: 'Web SQL',
        estimedSize: 010000000 // Nomenclatura octal, 8 bits
    };
    ng.module('websql', [])
        .value('database',
            window.openDatabase(
                dbInfo.name,
                dbInfo.version,
                dbInfo.displayName,
                dbInfo.estimedSize
            ))
    
        //só registrando as coisas no angular
        .factory('SQLFactory', SQLFactory)
        .service('SQLManager', SQLManager)
        .controller('SQLController', SQLController)
        .value('results', [])
    ;    


    (function extensionMethods() {

        Array.prototype.clear = function() {
            this.splice(0, this.length);
        };


        Array.prototype.remove = function (item) {
            var index = this.indexOf(item);
            if (index >= 0) this.splice(index, 1);
        };
        Array.prototype.contains = function (item) {
            return this.indexOf(item) >= 0;
        };
    })();


})(window.angular);