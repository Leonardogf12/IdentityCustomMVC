$(document).ready(function () {

    CreateDataTableProducts();   
});

function CreateDataTableProducts() {

    $("#dtProducts").DataTable({
        "responsive": false,
        "lengthChange": true,
        "autoWidth": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        'lengthMenu': [[10, 20, 30, 40, -1], [10, 20, 30, 40, "Todos"]],
        columnDefs: [
            { width: 10, targets:0 },
        ],
        "language": {
            "buttons": {
                "copy": "Copiar",
                "csv": "CSV",
                "excel": "Excel",
                "pdf": "PDF",
                "print": "Print",
                "colvis": "Ordenar Colunas",
            },
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Nenhum registro foi encontrado",
            "sEmptyTable": "Nenhum dado disponivel para a tabela",
            "sInfo": "Registros de _START_ a _END_ de um total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros de 0 a 0 de um total de 0 registros",
            "sInfoFiltered": "(filtrado de um total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Carregando...",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sLast": "Último",
                "sNext": "Próximo",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Ativar para ordenar a coluna de maneira ascendente",
                "sSortDescending": ": Ativar para ordenar a coluna de maneira crescente"
            },
        },
    }).buttons().container().appendTo('#dtProducts_wrapper .col-md-6:eq(0)');
}

function DeleteProductsIndex() {
    var registers = CheckItensSelected();

    if (registers == '') {

        swal.fire({
            icon: 'info',
            title: "<h3>Atenção</h3>",
            text: 'Selecione ao menos um registro.',
            showCancelButton: true,
            showConfirmButton: false,
            cancelButtonColor: '#d33',
            cancelButtonText: 'Fechar',
        });


    } else {

        Swal.fire({
            icon: 'question',
            title: '<h3>Excluir</h3>',
            text: 'Deseja realmente excluir este(s) registro(s)?',
            showCancelButton: true,
            confirmButtonColor: '#36c6d3',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Confirmar',
            footer: '<p>Obs: Não será possível recuperar um registro excluído.</label>'
        }).then(function (result) {
            if (result.value) {

                DeleteSeveral(registers);
            }
        });
    }
}

function DeleteSeveral(register) {
   
    var url = "/Products/DeleteSeveral";
   
    $.ajax({
        url: url,
        type: 'POST',      
        data: { _registers: register },
        beforeSend: function () {
        },
        success: function (data) {
            
            Swal.fire({
                icon: 'success',
                title: '<h3>Sucesso</h3>',
                text: 'Registro excluído com sucesso.',
                showConfirmButton: false,
                timer: 2000
            }).then(function () {
              
                $('#dtProducts').DataTable().clear().destroy();

                $("#listProductsRegisters").html('');
                $("#listProductsRegisters").html(data);

                CreateDataTableProducts();

            });
        },
        error: function (data) {
          
            swal.fire({
                icon: 'error',
                title: "<h3>Excluir</h3>",
                text: data.responseJSON,
                showCancelButton: true,
                showConfirmButton: false,
                cancelButtonColor: '#d33',
                cancelButtonText: 'Fechar',
                footer: '<a href="Products/Index">Produtos</label>'
            });

            //ToastCustom(1, "error", data.responseText);
        }
    });
}

function CheckItensSelected() {

    var checkboxes = document.getElementsByName('LISTACHECK');

    var registers = '';

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked == true) {
            registers = registers + checkboxes[i].id + ",";
        }
    }

    return registers;
}

function DeleteConfirm(register) {

    Swal.fire({
        icon: 'question',
        title: '<h3>Excluir</h3>',
        text: 'Deseja realmente excluir este(s) registro(s)?',
        showCancelButton: true,
        confirmButtonColor: '#36c6d3',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Confirmar',
        footer: '<p>Obs: Não será possível recuperar um registro excluído.</label>'
    }).then(function (result) {
        if (result.value) {           
            DeleteSeveral(register);
        }
    });
}

//*FUNCOES RESPONSAVEIS PELO CHECKBOX DO DATATABLE.
$('#mycheck').on('ifClicked', function (event) { checkAll(); });

$("#checkAll").click(function () {
    $('input:checkbox').not(this).prop('checked', this.checked);
});

function checkAll() {
    var checkboxes = document.getElementsByName('LISTACHECK');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].type == 'checkbox') {
           
            if ($('#mycheck').checked == true) {
                $('#' + checkboxes[i].id).attr('checked', true);
            }
            else {
                $('#' + checkboxes[i].id).attr('checked', false);
            }
        }
    }
}

