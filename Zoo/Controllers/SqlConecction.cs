namespace Zoo.Controllers
{
    internal class SqlConecction : Sqlconnection
    {
        private string cadena;

        public SqlConecction(string cadena)
        {
            this.cadena = cadena;
        }
    }
}