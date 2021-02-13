using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace PP3.DatabaseContext
{
    public class MessageRepo
    {
        public MessageModel GetById(int id)
        {
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();
            var message = connection
                .Query<MessageModel>("SELECT * FROM tbl_message WHERE message_id = @Id", new { Id = id })
                .SingleOrDefault();
            //Simulate long-running query
            Task.Delay(1000).Wait();
            connection.Close();
            return message;
        }

        public void DeleteMessage(int id)
        {
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();
            connection.Execute("DELETE tbl_message WHERE message_id = @Id", new {Id = id});
            connection.Close();
        }

        public void DeleteMessageExtension(int id)
        {
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();
            //Simulate long-running filtering
            Task.Delay(2000).Wait();
            connection.Execute("DELETE tbl_message_extension WHERE message_id = @Id", new { Id = id });
            connection.Close();
        }
    }
}
