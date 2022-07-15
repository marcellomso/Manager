# Manager
Desenvolver uma API para gerenciar o controle de pedidos de uma concessionária. Para isso, torna-se necessário os
seguintes cadastros:

• Cadastro de veículos

• Cadastro de vendedores

• Cadastro de oportunidades

O cadastro de veículos é necessário para que os carros possam ser comercializados e deve ter informações como nome,
ano, modelo, combustível, status, valor, entre outros. Lembrando que um veículo só pode ser comercializado uma vez.
Caso ele seja vendido novamente, deve-se criar um novo registro.
O cadastro dos vendedores é importante para que seja possível identificar quais vendedores criaram as oportunidades
e quais foram os com melhor performance de vendas. Para o cálculo das comissões, deve-se respeitar a seguinte tabela:

Cargo Comissão (%):

Vendedor Júnior 5

Vendedor Pleno 7,5

Vendedor Sênior 11

Além disso, é possível cadastrar uma comissão específica para um determinado vendedor (que pode ser entre 5 e 18%)
e este valor deve ser colocado no cadastro. Caso esteja configurado, este valor deve ser utilizado para o cálculo da
comissão. Caso contrário, utilizar o valor do cargo.

O registro de oportunidades deve contemplar a criação das oportunidades no sistema, ou seja, toda vez que alguém
fizer alguma proposta por algum veículo, o sistema deve registrar dados como veículo, vendedor, data, data de
expiração, status, entre outros. Os status aceitos para oportunidades são os seguintes:

• Criada

• Expirada (não pode ser alterado)

• Aceita (não pode ser alterado)

• Cancelada (não pode ser alterado)

Algumas regras para o cadastro de oportunidades:

• Todas as oportunidades devem ser iniciadas com o status Criada

• O valor da oportunidade pode ser informado pelo vendedor

• Cada oportunidade pode ter apenas um veículo

• Cada oportunidade tem um vendedor específico

Também deve ser possível alterar o status da proposta. Neste caso, deve-se registrar a mudança de status e o log de
alterações com a data e o novo status. Antes de alterar o status da proposta, deve-se validar a validade da mesma. Além
disso, é importante salientar que um vendedor não pode manipular e visualizar dados de uma oportunidade criada por
outro.

Observações Gerais:

Todo o acesso a API deve ser sigiloso.

O sistema deve manter, independente das exclusões, os registros de vendedores, veículos e oportunidades aceitas.
Também deve ser entregue a documentação da Api.
