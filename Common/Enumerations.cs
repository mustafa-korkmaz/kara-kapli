namespace Common
{
    public enum SortType
    {
        None,
        Ascending,
        Descending
    }

    public enum Status : byte
    {
        Active,
        Passive,
        Suspended,
        Deleted
    }

    public enum ResponseType
    {
        InternalError = -3,
        ValidationError = -2,
        Fail = -1,
        Success = 0,
        Warning = 1,
        Info = 2,
        NoEffect = 3,
        DuplicateRecord = 4,
        RecordNotFound = 5,
    }

    public enum PlanType
    {
        Free,
        Core,
        Plus,
        Advanced
    }

    public enum NotificationStatus : byte
    {
        Initial = 0,
        WaitingForSendingNotification = 1,
        NotificationSent = 2,
        NotificationRead = 3,
        HasError = 4
    }

    public enum DeviceType : byte
    {
        Ios = 0,
        Android = 1,
        None = 2
    }

    public enum RequestType : byte
    {
        Complaint = 0,
        Suggession = 1,
        Wish = 2,
        Other = 3,
        None = 4
    }

    public enum NotificationType : byte
    {
        General = 0,
        Suggestion = 1,
        Warning = 2,
        /// <summary>
        /// sent when a user is not fullfilled all the required profile info fields.
        /// </summary>
        UncompletedProfileInfo = 3,
        /// <summary>
        /// school or teacher profile activated notification
        /// </summary>
        ProfileActivated = 4,
        /// <summary>
        /// school or teacher profile declined notification
        /// </summary>
        ProfileDeclined = 5,
        /// <summary>
        /// a new product has been purchased
        /// </summary>
        ProductPurchased = 6,
        /// <summary>
        /// Approved teacher approved a document
        /// </summary>
        TeacherDocumentUploaded = 7,

        /// <summary>
        /// Approved teacher uploaded a form reference
        /// </summary>
        TeacherReferenceUploaded = 8,
        /// <summary>
        /// new school registered notification
        /// </summary>
        NewSchoolRegistered = 9,
        /// <summary>
        /// a new product has been purchased but no money has been paid yet
        /// </summary>
        ProductPurchasedWithWireTransfer = 10,
        /// <summary>
        /// a new order link has been generated
        /// </summary>
        OrderLinkGenerated = 11,
        /// <summary>
        /// a teacher applied for a job ad
        /// </summary>
        TeacherAppliedForAJob = 12,
        /// <summary>
        /// a school tried to hire a teacher
        /// </summary>
        SchoolHiredATeacher = 13
    }


    public enum LogType
    {
        /// <summary>
        /// use this type when you are initializing an object and want to log it.
        /// </summary>
        Initialize,
        /// <summary>
        /// use this type when you are creating an object and want to log it.
        /// </summary>
        Create,
        /// <summary>
        /// use this type when you are soft-deleting an object and want to log it.
        /// </summary>
        SoftDelete,
        /// <summary>
        /// use this type when you are hard-deleting an object and want to log it.
        /// </summary>
        HardDelete,
        /// <summary>
        /// use this type when you are editing an object and want to log it.
        /// </summary>
        Modify,
        /// <summary>
        /// use this type when you are logging a req & resp
        /// </summary>
        ReqAndResp,
        /// <summary>
        /// use this type when you are logging an exception
        /// </summary>
        Error
    }
}