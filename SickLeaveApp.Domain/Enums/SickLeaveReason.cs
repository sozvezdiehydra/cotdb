using System.ComponentModel;

namespace SickLeaveApp.Domain.Enums;

public enum SickLeaveReason
{
    [Description("01 - Заболевание")]
    Illness = 1,
        
    [Description("02 - Травма")]
    Injury = 2,
        
    [Description("03 - Карантин")]
    Quarantine = 3,
        
    [Description("05 - Беременность и роды")]
    Maternity = 5,
        
    [Description("09 - Уход за больным членом семьи")]
    CareForSickFamilyMember = 9
}