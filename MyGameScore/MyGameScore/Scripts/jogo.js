$(function(){
	
	$('#btnSalvarJogo').click(function(){
		
		var dataJogo = $('#dataJogoInput').val();
		var pontuacaoJogo = $('#pontuacaoInput').val();
		var serviceURL = 'http://localhost:49619/Home/SalvarJogo/';
		
		$.ajax({
			url:serviceURL,
			type:'POST',
			data:{data:dataJogo,pontuacao:pontuacaoJogo}
		}).done(function(retorno) {			

			var retorno = retorno;
			if(retorno) {
				alert('Seus pontos foram lançados com sucesso!');
			}
			else {
				alert('deu erro');
			}
		}).fail(function(err){
			console.log(err);
		});
	});
	
})