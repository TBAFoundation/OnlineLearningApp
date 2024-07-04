using System.ComponentModel;

namespace OnlineLearningApp;

public enum CourseCategory
{
    [Description("Data Science")]
    DataScience = 1,
    [Description("Data Analysis")]
    DataAnalysis,
    [Description("Programming")]
    Programming,
    [Description("Machine Learning")]
    MachineLearning,
    [Description("Artificial Intelligence")]
    AI,
    [Description("Web Development")]
    WebDevelopment,
    [Description("Mobile Development")]
    MobileDevelopment,
    [Description("Database Management")]
    DatabaseManagement,
    [Description("Cyber Security")]
    CyberSecurity,
    [Description("Cloud Computing")]
    CloudComputing,
    [Description("DevOps")]
    DevOps,
    [Description("Software Testing")]
    SoftwareTesting,
    [Description("Network Engineering")]
    NetworkEngineering,
    [Description("Project Management")]
    ProjectManagement,
    [Description("Business Analysis")]
    BusinessAnalysis,
    [Description("IT Support")]
    ITSupport,
    [Description("Game Development")]
    GameDevelopment,
    [Description("Internet of Things (IoT)")]
    IoT,
    [Description("Blockchain")]
    Blockchain,
    [Description("Big Data")]
    BigData,
    [Description("Robotics")]
    Robotics,
    [Description("Computer Vision")]
    ComputerVision,
    [Description("Natural Language Processing")]
    NaturalLanguageProcessing,
    [Description("Quantum Computing")]
    QuantumComputing,
    [Description("Virtual Reality")]
    VirtualReality,
    [Description("Augmented Reality")]
    AugmentedReality
}
