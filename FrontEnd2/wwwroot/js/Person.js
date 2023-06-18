var PersonModule = (function () {


    function Deletar(id, name) {
        $('#deleteModal').modal('show');
        $('#inputid').val(id);
        $('#messageDelete-message').text('Tem certeza que quer deletar' + ' ' + name + ' ?');
    }

    function Deletar2(id) {

        $.ajax({
            url: "/Person/DeletePerson",
            method: "POST",
            data: {
                Id: id
            },
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            success: function (result) {
                if (result.success) {
                    $('#deleteModal').modal('hide');
                    alert("conta deletada com sucesso");
                    ResetCache();

                }
                else {
                    alert(result.message)
                }
            }
        });
    }

    function Details(id) {

        $.ajax({
            url: "/Person/PersonDetails",
            method: "POST",
            data: {
                Id: id
            },
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            success: function (result) {
                if (result.success) {
                    $('#personTaxNumberDetail').text('CPF: ' + result.data.taxNumber)
                    $('#personNameDetail').text('Nome: ' + result.data.personName)
                    $('#DetailsModal').modal('show');
                }
                else {
                    alert(result.message)
                }
            }
        });
    }

    function Create() {
        let person = $("#personNameCreate").val();
        let taxNumber = $("#personTaxNumberCreate").val();

        if (person.length > 5 && (taxNumber.length >= 11 && taxNumber.length <= 14)) {
            $.ajax({
                url: "/Person/AddNewPerson",
                method: "POST",
                data: {
                    PersonName: person, TaxNumber: taxNumber
                },
                headers: {
                    RequestVerificationToken: $("#RequestVerificationToken").val()
                },
                success: function (result) {
                    if (result.success == 'true') {
                        $("#personNameCreate").val('');
                        $("#personTaxNumberCreate").val('');

                        ResetCache();

                        alert("conta criada com sucesso")

                    }
                    else {
                        alert("Error")
                    }
                }
            });
        }
        else
            alert("Campos não possuem quantidade minima para cadastro")
    }
    function Esconder() {
        var tableBody = $('#personTableBody');
        tableBody.empty();
    }
    function ResetCache() {
        $.ajax({
            url: '/Person/ResetCache',
            method: 'POST',
            dataType: 'json',
            success: function (response) {

                if (response.success) {
                    var tableBody = $('#personTableBody');
                    tableBody.empty();
                    Carregar()
                }

            }
        });

    }

    function Carregar() {
        $(document).ready(function () {
            $.ajax({
                url: '/Person/CarregarTeste',
                method: 'POST',
                dataType: 'json',
                success: function (response) {
                    var data = response.data;
                    var tableBody = $('#personTableBody');
                    tableBody.empty();


                    $.each(data, function (index, person) {
                        var id = person.personId;
                        var personName = person.personName;
                        var taxNumber = person.taxNumber;

                        var row = $('<tr>');

                        var buttonDetail = $('<button type="button" class="btn btn-success mr-2 text-center" data-id="' + id + '" onclick="PersonModule.Details(this.dataset.id)">Detalhes</button>');

                        var buttonDelete = $('<button type="button" class="btn btn-danger text-center" data-id="' + id + '" data-nome="' + personName + '" onclick="PersonModule.Deletar(this.dataset.id,this.dataset.nome)" data-target="#deleteModal" data-toggle="modal">Remover</button>');
                        var nameColumn = $('<td class="text-center">').text(personName);
                        var ageColumn = $('<td class="text-center">').text(taxNumber);

                        row.append(nameColumn);
                        row.append(ageColumn);
                        row.append($('<td class="text-center">').append(buttonDetail).append(buttonDelete));

                        tableBody.append(row);
                    });
                }
            });
        });
    }

    return {
        Deletar: Deletar,
        Deletar2: Deletar2,
        Details: Details,
        Create: Create,
        Carregar: Carregar,
        Esconder: Esconder,
        ResetCache: ResetCache
    };
})();

