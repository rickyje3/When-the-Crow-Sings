using UnityEngine;

[System.Serializable]
public class SubSceneLogicBase
{
    public string associatedDataKey = "";
    public enum VALUE_TYPE { BOOL, INT }
    public VALUE_TYPE valueType = VALUE_TYPE.BOOL;
    [Header("If BOOL, then fill this.")]
    public bool boolValue;
    [Header("If INT, then fill this.")]
    public int intValue;
    public enum OPERATOR { EQUALS, LESS_THAN, GREATER_THAN };
    public OPERATOR associatedOperator = OPERATOR.EQUALS;
}
