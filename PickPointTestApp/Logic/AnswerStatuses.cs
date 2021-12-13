using PickPointTestApp.Logic.Models;

namespace PickPointTestApp.Logic
{
    public struct AnswerStatuses
    {
        public static AnswerStatus Created { get { return new AnswerStatus { ResultCode = true, ResultStatus = "Created" }; } }
        public static AnswerStatus Edited { get { return new AnswerStatus { ResultCode = true, ResultStatus = "Edited" }; } }
        public static AnswerStatus Cancelled { get { return new AnswerStatus { ResultCode = true, ResultStatus = "Cancelled" }; } }
        public static AnswerStatus NotFound { get { return new AnswerStatus { ResultCode = false, ResultStatus = "Not Found" }; } }
        public static AnswerStatus TooManyItems { get { return new AnswerStatus { ResultCode = false, ResultStatus = "Too Many Items" }; } }
        public static AnswerStatus TooFewItems { get { return new AnswerStatus { ResultCode = false, ResultStatus = "Too Few Items" }; } }
        public static AnswerStatus WrongFormat { get { return new AnswerStatus { ResultCode = false, ResultStatus = "Wrong Format" }; } }
        public static AnswerStatus Forbidden { get { return new AnswerStatus { ResultCode = false, ResultStatus = "Forbidden" }; } }
    }
}
