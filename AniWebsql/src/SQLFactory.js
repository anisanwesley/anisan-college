
//Minha montadora de SQL
function SQLFactory() {
    
    //metodos aqui, retorno isso no final
    var fac = {
        create: create,
        insert: insert,
        select: select,
        drop: drop,
        delete: deleteCmd
    };


    //Private Methods
    function create(options) {

        var sql = 'CREATE TABLE IF NOT EXISTS ';
        sql += options.table;

        sql += " (";

        for (var i in options.columns) {
            var column = options.columns[i];

            sql += column.name;

            if(i<options.columns.length-1)sql+= ", ";


        }
        sql += ")";
        return log(sql);
    }

    function insert(options) {

        var sql = 'INSERT INTO ';
        sql += options.table;

        sql += " (";

        sql += catchProperty(options.columns, 'name');

        sql += ') VALUES (';

        sql += catchProperty(options.columns, 'data');
        sql += ")";

        return log(sql);
    }

    function select(options) {
        var sql = "SELECT ";
        sql += options.columns || '*';
        sql += ' FROM ' + options.table;
        sql += catchConditions(options.conditions);
        return log(sql);
    }

    function drop(options) {
        return log("DROP TABLE " + options.table);
    }

    function deleteCmd(options) {
        var sql = "DELETE ";
        sql += 'FROM ' + options.table;

        sql += catchConditions(options.conditions);
        return log( sql);
    }


    //Private methods

    //pega todas as condições de um WHERE
    function catchConditions(conditions) {

        var sql = "";
        if (conditions&&conditions.length) {
            sql += " WHERE ";
            for (var i = 0; i < conditions.length; i++)
                sql += conditions[i].name + " ";
        }
        return sql;
    }

    //pega todas as propriedades de determinado array
    function catchProperty(array, property) {
        var sql = "";
        for (var i in array) {
            var item = array[i];
            sql += i === '0' ? "" : ", ";
            sql += item[property] + " ";
        }
        return sql;
    }

    function log(sql) {
        toastr.info(sql);
        return sql;
    }

    return fac;

}