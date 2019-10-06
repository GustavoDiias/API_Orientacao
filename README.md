# API_Orientação
API em C#

<h2>Dependências NuGET:</h2>
<ul>
  <li>Mircrosoft AspNetCore.All</li>
  <li>Microsoft Entity Framework Core</li>
  <li>Microsoft Visual Studio Web Code Generation Design</li>
  <li>Swashbuckle Aps Net Core</li>
</ul>

<h2>Migration</h2>
<ol>
  <li>Trocar no <b>appsettings.json</b> o caminho do Banco</li>
  <li>Trocar no <b>appsettings Development.json</b> o caminho do Banco</li>
  <li>Executar no promp do NuGet <b>remove-migration</b></li>
  <li>Executar no promp do NuGet <b>add-migration (NomeDaMigration)</b></li>
  <li>Executar no promp do NuGet <b>update-database (NomeDaMigration)</b></li>
</ol>
    
<h2>Ordem de Mapeamento</h2>
<ol>
  <li>Criação das Entidades na ApiOrientacao.DATA</li>
  <li>Adicionando as Entidades no Contexto na APIOrientacao.DATA</li>
  <li>Criar o Request de cada Entidade na ApiOrientacao(Obs: Os ID auto incremento nao precisam ser adicionados)</li>
  <li>Criar o Response de cada Entidade na ApiOrientacao.(Obs: Todos os valores)</li>
  <li>Criar os Controlladores para cada Entidade na ApiOrientacao</li>
</ol>
