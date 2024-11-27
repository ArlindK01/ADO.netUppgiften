using Microsoft.Data.SqlClient;

namespace ADO.netUppgiften
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            Console.WriteLine("Ange skådespelarens förnamn:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Ange skådespelarens efternamn:");
            var lastName = Console.ReadLine();

            var query = @"
                SELECT film.title 
                FROM film
                INNER JOIN film_actor ON film.film_id = film_actor.film_id
                INNER JOIN actor ON film_actor.actor_id = actor.actor_id
                WHERE actor.first_name = @FirstName AND actor.last_name = @LastName";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName.ToUpper());
            command.Parameters.AddWithValue("@LastName", lastName.ToUpper());


            connection.Open();
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine($"Filmer där {firstName} {lastName} har deltagit:");
                while (reader.Read())
                {
                    Console.WriteLine(reader["title"]);
                }
            }
            else
            {
                Console.WriteLine($"Inga filmer hittades för {firstName} {lastName}.");
            }

            connection.Close();
        }

    }
}
