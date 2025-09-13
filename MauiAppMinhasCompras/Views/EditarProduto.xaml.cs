using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras.Views;

// Define a classe da página EditarProduto, que permite editar os dados de um produto existente
public partial class EditarProduto : ContentPage
{
    // Construtor da página
    public EditarProduto()
	{
		InitializeComponent();

        // Preenche o Picker com as categorias padrão
        pickerCategoria.ItemsSource = CategoriaHelper.CategoriasPadrao;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Recupera o produto vinculado à tela
        Produto produto = BindingContext as Produto;

        if (produto != null)
        {
            // Preenche os campos com os dados do produto
            txt_descricao.Text = produto.Descricao;
            txt_quantidade.Text = produto.Quantidade.ToString();
            txt_preco.Text = produto.Preco.ToString();

            // Seleciona a categoria atual no Picker
            pickerCategoria.SelectedItem = produto.Categoria;


        }

    }
    // Evento chamado quando o botão "Salvar" da Toolbar é clicado. Usado para configurar o salvar a edição.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,                
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = pickerCategoria.SelectedItem?.ToString()
            };

            // Utilizou o metodo UPDATE. Diferente do novo produto onde usamos o metod INSERT.
            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}