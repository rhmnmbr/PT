using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTApi.Data.Models
{
    public enum ObjState { Unloaded, Loaded, Modified, New }

    public class BaseData
    {
        protected ObjState state = ObjState.Unloaded;

        private string userid = "aman";

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

        [NotMapped]
        public ObjState State
        {
            get { return state; }
            set { state = value; }
        }

        public bool ConfirmUserID(User user)
        {
            userid = user.Id;
            return true;
        }

        public string GetUserId()
        {
            return userid;
        }
    }
}