// using Java.Lang.Reflect;
using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // 🔒 Campo privado e estático que armazenará a instância única do banco de dados
                static SQLiteDatabaseHelper _db;

        // 🗂 Propriedade pública que expõe o banco de dados para o restante do app
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // 🧠 Verifica se o banco já foi instanciado
                if (_db == null)
                {
                    // 📁 Define o caminho físico onde o arquivo do banco será salvo
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    // 🏗 Cria a instância do helper, que gerencia o CRUD
                    _db = new SQLiteDatabaseHelper(path);
                }
                //🔁 Retorna a instância existente(singleton)
                return _db;
            }
        }
        // 🚀 Construtor principal da aplicação
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage (new Views.ListaProduto());

        }
    }
}
