# myGameScore
myGameScore

- Foi criado a aplicação utilizando as seguintes tecnologias no Visual Studio: C#, ASP.NET MVC, Razor Pages, JavaScript, Ajax, Entity Framework, HTML5 e SQL Server. Além dos requisitos solicitados no teste, foi adicionado as funcionalidades de listar todos os jogos cadastrados (ordenados por data), editar um jogo cadastrado e excluir um jogo.

- Como foi usado o Entity Framework, o banco de dados é criado automaticamente local ao executar a aplicação no Visual Studio, sendo assim a única configuração necessária é alterar a ConnectionString apontando o nome da Instancia do banco local.

Arquivo: \MyGameScore\Web.config
Alterar a instancia na tag 'connectionString' dentro do atributo 'data source':

"add name="database" connectionString="data source=LUCAS\SQLEXPRESS;initial catalog=MyGameScore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"
  
- No arquivo \MyGameScore\Scripts\jogo.cs existe a váriavel com a URL Base para efetuar as requisições na aplicação, sendo assim é necessário revisar se esta de acordo com o LocalHost do ambiente, se necessário alterar a porta:
 
 Arquivo: \MyGameScore\Scripts\jogo.cs
 Váriavel: var baseUrl = 'http://localhost:49619';
