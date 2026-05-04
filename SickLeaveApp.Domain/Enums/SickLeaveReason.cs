namespace SickLeaveApp.Domain.Enums;

public enum SickLeaveReason
{
    // Заболевание
    Illness = 1,
        
    // Травма 
    Injury = 2,
        
    // Карантин 
    Quarantine = 3,
        
    // Уход за больным членом семьи 
    CareForSickFamilyMember = 9,
        
    // Отпуск по беременности и родам
    Maternity = 5
}