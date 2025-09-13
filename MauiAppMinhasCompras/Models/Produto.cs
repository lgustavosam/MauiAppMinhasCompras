using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // Define a classe Produto, que representa um item cadastrado no banco de dados
    public class Produto
    {
        // Campo privado para armazenar a descrição com validação personalizada
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Propriedade Descricao com validação: impede que seja nula
        public string Descricao {
            get => _descricao; //Para inserção um novo, não precisa validação.
            set
            {
                // Validação simples para garantir que a descrição seja preenchida
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }

        // Quantidade do produto (sem validação aqui, mas pode ser feita externamente)
        public double Quantidade { get; set; }

        // Preço unitário do produto
        public double Preco { get; set; }

        // Categoria do produto
        public string Categoria { get; set; }


        // Propriedade calculada que retorna o valor total (quantidade × preço)
        public double Total { get => Quantidade * Preco; }
    }
}
