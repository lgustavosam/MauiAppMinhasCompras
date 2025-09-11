using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

// Define a classe da página ListaProduto, que exibe os produtos cadastrados
public partial class ListaProduto : ContentPage
{
    // Coleção observável que será exibida na interface e atualizada dinamicamente
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {

        InitializeComponent(); // Inicializa os componentes visuais definidos no XAML

        // Define a fonte de dados da lista visual (provavelmente um CollectionView ou ListView)
        lst_produtos.ItemsSource = lista;
    }

    // Evento chamado sempre que a página aparece na tela (ideal para atualizar os dados)
    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear(); // Limpa a lista atual

            List<Produto> tmp = await App.Db.GetAll(); // Busca todos os produtos do banco

            tmp.ForEach(i => lista.Add(i));// Adiciona cada produto à ObservableCollection

        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso algo falhe
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
    // Evento chamado ao clicar no botão "Adicionar" da Toolbar
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a tela de cadastro de novo produto
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Evento chamado sempre que o texto da SearchBar é alterado
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear(); // adicionado para que toda vez que edite, ao reabria lista, não duplique os dados.

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    // Evento chamado ao clicar no botão "Somar" da Toolbar
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    // Evento chamado ao clicar em "Remover" no menu de contexto de um item
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            //sempre que eu clicar no produto do menu item, vai me dar a opção a seguir.
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            // Confirma com o usuário se deseja remover
            bool confirm = await DisplayAlert(
                // foi personalizado aparecer nome do item.
                "Tem certeza", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
    // Evento chamado ao selecionar um item da lista. Usado para EDITAR um item da lista
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            // Ao clicar no item ele irá para o editar produto
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,

            });

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    //Usado para atualizar a lista.
    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear(); // Limpa a lista atual

            List<Produto> tmp = await App.Db.GetAll(); // Busca todos os produtos do banco

            tmp.ForEach(i => lista.Add(i));// Adiciona cada produto à ObservableCollection

        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso algo falhe
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally // Este bloco é sempre executado para fechar o recurso, se ele foi aberto
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}