using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace PP3.DatabaseContext
{
    public class FixedMessageRepo
    {
        private readonly Semaphore _semaphore;
        public FixedMessageRepo(int connectionLimit)
        {
            _semaphore = new Semaphore(connectionLimit, connectionLimit);
        }
        public MessageModel GetById(int id)
        {
            _semaphore.WaitOne();
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();

            var message = connection
                .Query<MessageModel>("SELECT * FROM tbl_message WHERE message_id = @Id", new { Id = id })
                .SingleOrDefault();
            //Simulate long-running query
            Task.Delay(1000).Wait();
            connection.Close();
            _semaphore.Release();
            return message;
        }

        public void DeleteMessage(int id)
        {
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();
            connection.Execute("DELETE tbl_message WHERE message_id = @Id", new { Id = id });
            connection.Close();
        }

        public void DeleteMessageExtension(int id, Action commit)
        {
            var connection = new SqlConnection("Server=.;Database=PP;Trusted_Connection=True");
            connection.Open();
            //Simulate long-running filtering
            Task.Delay(2000).Wait();
            connection.Execute("DELETE tbl_message_extension WHERE message_id = @Id", new { Id = id });
            connection.Close();
            commit.Invoke();
        }
    }
}
