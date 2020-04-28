namespace Common
{
    public enum AuthenticationMode
    {
        Network = 0,
        Application = 1,
        Both = 2,
        None = 3,
        BothSecure = 4
    }

    public enum EmailSearchType
    {
        All,
        Confirmed,
        NonConfirmed
    }

    public enum JobPostSearchType
    {
        All,
        Active,
        Passive
    }

    public enum EmailConfirmationStatus
    {
        None,
        Confirmed,
        NotConfirmed
    }

    public enum Status : byte
    {
        Active,
        Passive,
        Suspended,
        Deleted
    }

    public enum CampaignFilterType : byte
    {
        /// <summary>
        /// page name filter. eg: teacher-premium, school-premium
        /// </summary>
        Page = 1,
        /// <summary>
        /// user product plan filter. eg: advance, core
        /// </summary>
        Plan = 2,
        /// <summary>
        /// user type filter. eg: teacher, school
        /// </summary>
        UserType = 3,
        /// <summary>
        /// all users, authed users or guests?
        /// </summary>
        Authentication
    }

    public enum Key
    {
        Tag,
        Discount,
        Image,
        Video
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

    public enum WebMethodType
    {
        Null,
        Get,
        Post,
        Put,
        Info,
        NoEffect
    }

    public enum ContentType
    {
        Null,
        FormUrlencoded
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

    public enum PageType : byte
    {
        PrudentialFirstWeb = 1,
        PrudentialFirstAdmin = 2,
        PrudentialFirstMobile = 3
    }

    /// <summary>
    ///  dbo.UserSettings.Key 
    /// </summary>
    public enum UserSettingKey : byte
    {
        AuthorNotifications = 0
    }

    public enum SearchType
    {
        Authors = 1,
        Events = 2
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

    public enum StudentNotificationMailType : byte
    {
        None = 0,
        CompleteProfile = 1,
        ApplicationFee = 2,
        UploadDocs = 3,
        FormSubmissions = 4,
        Generic = 5,
        Custom = 6
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

    public enum PoolActionType
    {
        Removed = 0,
        Added = 1
    }

    public enum UserLogType : byte
    {
        None = 0,
        ProfileApproved = 1,
        ProfileDeclined = 2,
        InProgress = 3,
        InProgressCompleted = 4,
        Passive = 5,
        BackgroundDocumentVerified = 6,
        BackgroundDocumentUnverified = 7,
        ResumeVerified = 8,
        ResumeUnverified = 9,
        IdentityVerified = 10,
        IdentityUnverified = 11,
        CitizenshipVerified = 12,
        CitizenshipUnverified = 13,
        DegreesVerified = 14,
        DegreesUnverified = 15,
        CertificationVerified = 16,
        CertificationUnverified = 17,
        ReferenceApproved = 18,
        ReferenceCallAgain = 19,
        ReferenceDeclined = 20,
        ReferencesVerified = 21,
        ReferencesUnverified = 22,
        WhatsappInteraction = 23,
        WechatInteraction = 24,
        EmailInteraction = 25,
        PhoneInteraction = 26,
        CellPhoneInteraction = 27,
        SkypeInteraction = 28,
        OtherInteraction = 29,
        FormReferenceApproved = 30,
        FormReferenceCallAgain = 31,
        FormReferenceDeclined = 32,
        ProductUpgradedToCore = 33,
        ProductUpgradedToPlus = 34,
        ProductUpgradedToAdvance = 35,
        TeacherDeleted = 36,
        SchoolDeleted = 37,
        Notes = 38,
        ReminderInteraction = 39,
        VideoVerified = 40,
        VideoUnverified = 41,
        NameSurnameUpdated = 42,
        CurrentCityUpdated = 43,
        TeacherDescriptionUpdated = 44,
        AchievementsUpdated = 45,
        PreviouslyWorkUpdated = 46,
        RelatedExperienceUpdated = 47,
        EducationDegreeUpdated = 48,
        TeachingExperienceUpdated = 49,
        StudentDeleted = 50,
        ApplicationStatusUpdated = 51,
        ReferenceLetterVerified = 52,
        ReferenceLetterUnverified = 53,
        TranscriptVerified = 54,
        TranscriptUnverified = 55,
        EssayVerified = 56,
        EssayUnverified = 57,
        TestScoreVerified = 58,
        TestScoreUnverified = 59,
        SchoolNameUpdated = 60,
        SchoolCityNameUpdated = 61,
        StudentNameSurnameUpdated = 62,
        StudentCurrentCityUpdated = 63,
        StudentPaymentOverridden = 64,
        InteractionAttemptFailed = 65,
        ApplicationFeeChargedBack = 66,
        DepositFeeChargedBack = 67,
        NotificationStatusChanged = 68,
        AddedToTeacherPool = 69,
        RemovedFromTeacherPool = 70,
        TeacherWireTransferApproved = 71,
        SchoolWireTransferApproved = 72,
        ProductAdded = 73,
        ProductUpdated = 74,
        ProductDeleted = 75,
        SchoolPropertyUpdated = 76
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

    public enum ScholarshipApplicationStatus
    {
        None = 0,
        Initial = 1,
        ApplicationFee = 2,
        UploadDocs,
        FormSubmission,
        AdminReview,
        SchoolReview,
        OfferSent,
        DepositeFee,
        TutionFee,
        AcceptanceLetter
    }
}