using System;

namespace PickPointTestApp.Logic.Models
{
    public class AnswerStatus
    {
        public AnswerStatus()
        {

        }

        public AnswerStatus(AnswerStatus baseStatus, string resultInfo)
        {
            this.ResultCode = baseStatus.ResultCode;
            this.ResultStatus = baseStatus.ResultStatus;
            this.ResultInfo = resultInfo;
        }

        public bool ResultCode { get; set; }
        public string ResultStatus { get; set; }
        public string ResultInfo { get; set; }

        public override bool Equals(Object obj)
        {
            var other = obj as AnswerStatus;
            if (other == null) return false;

            return Equals(other);
        }

        public bool Equals(AnswerStatus other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.ResultCode == other.ResultCode && this.ResultStatus == other.ResultStatus)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
