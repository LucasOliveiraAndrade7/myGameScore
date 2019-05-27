$(function(){
	
	
	//Salvar novo jogo

	$('#btnSalvarJogo').click(function(){
		
		var dataJogo = $('#dataJogoInput').val();
		var pontuacaoJogo = $('#pontuacaoInput').val();
		var serviceURL = 'http://localhost:49619/Home/SalvarJogo/';
		
		if (pontuacaoJogo == "")
		{
			swal(
				'Campo Obrigatório',
				'Preencha o campo Pontuação!',
				'error'
			)	
			return false;
		}		
		if (pontuacaoJogo < 0)
		{
			
			swal(
				'Atenção',
				'O valor da pontuação deve ser maior ou igual a zero!',
				'error'
			)	
			return false;
		}		
		if (dataJogo == "")
		{
			swal(
				'Campo Obrigatório',
				'Preencha o campo Data!',
				'error'
			)	
			return false;
		}
		
		$.ajax({
			url:serviceURL,
			type:'POST',
			data:{data:dataJogo,pontuacao:pontuacaoJogo}
		}).done(function(retorno) {			
			
			var retorno = retorno;
			if(retorno) {
				swal({
				  type: 'success',
				  title: "Jogo cadastrado com sucesso!",
				  showConfirmButton: true,
				}).then(function () {
					window.location.reload(true);
				})
			}
			else {
				swal(
				  'Oops...',
				  'Falha ao gravar os dados!',
				  'error'
				)
			}
		}).fail(function(err){
			console.log(err);
		});
	});
	
	//Excluir jogo
	
	$(document).on('click','.btnDeletarJogo',function(){

		var idJogo = $(this).closest('tr').attr('id');
		var serviceURL = 'http://localhost:49619/Home/ExcluirJogo/';

		swal({
			  title: 'Você tem certeza?',
			  text: "Você não poderá reverter isso!",
			  type: 'warning',
			  showCancelButton: true,
			  confirmButtonColor: '#3085d6',
			  cancelButtonColor: '#d33',
			  confirmButtonText: 'Sim, exclua-o!'
		}).then(function () {
			  
			$.ajax({
			url:serviceURL,
			type:'POST',
			data:{id:idJogo}

		}).done(function(retorno) {
				var retorno = retorno;
				if(retorno) {
					swal({
					  type: 'success',
					  title: "Jogo removido com sucesso!",
					  showConfirmButton: true,
					}).then(function () {
						window.location.reload(true);
					})
				}
				else {
					swal(
					  'Oops...',
					  'Falha ao excluir o jogo selecionado!',
					  'error'
					)
				}
			}).fail(function(err){
			console.log(err);
			});
		});
	});
	
	
	
	// Botao EDITAR

	$(document).on('click','.btnEditarJogo',function(){

		var idJogo = $(this).closest('tr').attr('id');
		var serviceURL = 'http://localhost:49619/Home/SelecionarJogo/';

		$.ajax({
			url:serviceURL,
			type:'POST',
			data:{id:idJogo}

		}).done(function(retorno){
			
		var response = JSON.parse(retorno);
		var id = response.results[0].id;
		var dataFormatada = formatDate(response.results[0].data_jogo);
		var pontuacao = response.results[0].pontuacao;
		
		
		document.getElementById("editIDJogoInput").value = id;
		document.getElementById("editDataJogoInput").value = dataFormatada;
		document.getElementById("editPontuacaoInput").value = pontuacao;	

		}).fail(function(err){
			console.log(err);
		});
	}); 
	
	function formatDate(date) {
		var d = new Date(date),
			month = '' + (d.getMonth() + 1),
			day = '' + d.getDate(),
			year = d.getFullYear();

		if (month.length < 2) month = '0' + month;
		if (day.length < 2) day = '0' + day;

		return [year, month, day].join('-');
	}
	
	//Editar jogo (Update)

	$('#btnEditarJogo').click(function(){
		
		var idJogo = $('#editIDJogoInput').val();
		var dataJogo = $('#editDataJogoInput').val();
		var pontuacaoJogo = $('#editPontuacaoInput').val();
		var serviceURL = 'http://localhost:49619/Home/EditarJogo/';
		
		if (pontuacaoJogo == "")
		{
			swal(
				'Campo Obrigatório',
				'Preencha o campo Pontuação!',
				'error'
			)	
			return false;
		}		
		if (pontuacaoJogo < 0)
		{
			
			swal(
				'Atenção',
				'O valor da pontuação deve ser maior ou igual a zero!',
				'error'
			)	
			return false;
		}		
		if (dataJogo == "")
		{
			swal(
				'Campo Obrigatório',
				'Preencha o campo Data!',
				'error'
			)	
			return false;
		}

		$.ajax({
			url:serviceURL,
			type:'POST',
			data:{id:idJogo,data:dataJogo,pontuacao:pontuacaoJogo}

		}).done(function(retorno) {
			
			var retorno = retorno;
			if(retorno) {
				swal({
				  type: 'success',
				  title: "Jogo alterado com sucesso!",
				  showConfirmButton: true,
				}).then(function () {
					window.location.reload(true);
				})
			}
			else {
				swal(
				  'Oops...',
				  'Falha ao alterar os dados!',
				  'error'
				)
			}
		}).fail(function(err){
			console.log(err);
		});
	});
	
	
})