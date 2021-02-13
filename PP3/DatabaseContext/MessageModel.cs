using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PP3.DatabaseContext
{
    [Table("tbl_message_extension")]
    public class MessageExtensionModel
    {
        [Column("message_id"), Key]
        public int MessageId { get; set; }
        [Column("additional_data")]
        public int AdditionalData { get; set; }
        [ForeignKey("MessageId")] 
        public MessageModel MessageModel { get; set; }
    }
}
