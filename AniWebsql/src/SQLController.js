//classe responsavel por interagir com o HTML
SQLController.$inject = ['SQLFactory', 'SQLManager', 'results'];
function SQLController(SQLFactory, SQLManager, results) {
    var self = this;
    self.supported = !!window.openDatabase;

    //properties
    //objetos que são usados para montar as queries
    self.create = {
        table: '',
        columns: [{name:''},{name:''}]
    };

    self.insert = {
        table: '',
        columns: [{ name: '', data: "" }]
    };
    self.select = {
        table: '',
        columns: "",
        conditions: []
    };
    self.delete = {
        table: '',
        conditions: []
    };
    self.drop = {
        table: ''
    };

    //public methods
    self.execute = function(cmd) {
        //monta a query
        var query = SQLFactory[cmd](self[cmd]);

        //depois da query montada, executa ela
        SQLManager.execute(query);

        //results é um objeto global que é populado la no SQLManager
        //esta sendo usado pra montar a table
        buildTable(results);
    };


    //private methods
    //aqui é JS puro
    function buildTable(results) {

        var container = document.querySelector('#container');
        container.innerHTML = "";

        //se nao tiver resultados, para por aqu
        if (!results.length) {


            var alert = document.createElement('div');
            alert.setAttribute('class', 'alert alert-danger');

            var textNode = document.createTextNode("Não trouxe resultados.");

            alert.appendChild(textNode);

            container.appendChild(alert);

            return;
        }

        var table = document.createElement('table');
        table.setAttribute("class", "table table-striped");


        //results virá assim = [{ "Nome": "anisan", "Idade": 21 }, { "Nome": "shyyaruku", "Idade": 13 }];

        //usa o primeiro objeto como cobaia para abstrair as propriedades
        var properties = [];
        for (var prop in results[0]) {
            properties.push(prop);

        }

        //thead
        var thead = document.createElement('thead');
        var tr = document.createElement('tr');
        for (var i = 0; i < properties.length; i += 1) {
            var td = document.createElement('td');
            var textNode = document.createTextNode(properties[i]);
            td.appendChild(textNode);
            tr.appendChild(td);

        }


        //tbody
        var tbody = document.createElement('tbody');
        for (var i = 0; i < results.length; i += 1) {
            var item = results[i];
            var tr2 = document.createElement('tr');

            for (var j = 0; j < properties.length; j += 1) {

                var td = document.createElement('td');
                var prop = properties[j];
                var textNode = document.createTextNode(item[prop]);

                td.appendChild(textNode);
                tr2.appendChild(td);

            }

            tbody.appendChild(tr2);
        }




        thead.appendChild(tr);
        table.appendChild(thead);
        table.appendChild(tbody);

        console.log(properties);
        container.appendChild(table);


    }


}