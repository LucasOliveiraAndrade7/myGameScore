$(function(){
	
	$('#btnSalvarJogo').click(function(){
		
		var dataJogo = $('#dataJogoInput').val();
		var pontuacaoI = $('#pontuacaoInput').val();
		var serviceURL = '/Home/About';
		
		$.ajax({
			url:serviceURL,
			type:'POST',
			data:{data_jogo:dataJogo,pontuacao:pontuacaoI},
			contentType: "application/json; charset=utf-8",
            dataType: "json"

		}).done(function(retorno) {			

			var retorno = retorno;
			if(retorno) {
				Alert('Seu Cliente foi salvo com Sucesso!');
			}
			else {
				Alert('deu erro');
			}
		}).fail(function(err){
			console.log(err);
		});
	});
	
})