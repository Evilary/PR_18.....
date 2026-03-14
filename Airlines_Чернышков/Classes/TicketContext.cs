using System;
using System.Collections.Generic;
using MySqlConnector; // или MySql.Data.MySqlClient
using Airlines_Чернышков.Models;

namespace Airlines_Чернышков.Classes
{
    public class TicketContext : Ticket
    {
        public TicketContext(int Price, string From, string To, DateTime StartTime, DateTime EndTime)
            : base(Price, From, To, StartTime, EndTime) { }

        public static List<TicketContext> AllTickets()
        {
            List<TicketContext> allTickets = new List<TicketContext>();

            string connStr = "server=localhost;port=3306;database=Airlines-chern;uid=root;pwd=root;";

            using (var connection = new MySqlConnection(connStr))
            {
                connection.Open();

                using (var command = new MySqlCommand("SELECT * FROM Tickets", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allTickets.Add(new TicketContext(
                            reader.GetInt32(3),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(4),
                            reader.GetDateTime(5)
                        ));
                    }
                }
            }

            return allTickets;
        }
    }
}