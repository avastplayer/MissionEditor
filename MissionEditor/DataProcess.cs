using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MissionEditor
{
    internal class DataProcess
    {
        public struct MissionField
        {
            int MissionID;
            string MissionName;
            string MissionTypeString;
            int MinLevel;
            int MaxLevel;
            int TransMinLevel;
            int TransMaxLevel;
            int[] RequestMissionList;
            long[] RequestRoleIDList;
            int[] PostMissionList;
            int TransformID;
            string NoteInfo;
            long ExpReward;
            long MoneyReward;
            long PetExpReward;
            int ShengWang;
            long SMoney;
            int RewardMapJumpType;
            int RewardMapID;
            int RewardMapXPos;
            int RewardMapYPos;
            int ProcessBarTime;
            string ProcessBarText;
            string ProcessBarColor;
            int[] DisPlayNPCID;
            int[] RewardItemIDList;
            int[] RewardItemNumList;
            int[] RewardItemShapeIDList;
            int[] RewardItemIsBindList;
            int MissionType;
            int ActiveInfoNpcID;
            int ActiveInfoMapID;
            int ActiveInfoLeftPos;
            int ActiveInfoTopPos;
            int ActiveInfoRightPos;
            int ActiveInfoBottomPos;
            int ActiveInfoTargetID;
            int ActiveInfoTargetNum;
            int ActiveInfoMiniStep;
            int ActiveInfoStepProbability;
            int ActiveInfoMaxStep;
            int ActiveInfoTeamState;
            int ActiveInfoTimeLimit;
            int ActiveInfoIsRestartTimer;
            long ActiveInfoGiveBackMoney;
            int ActiveInfoGiveBackPetID;
            int ActiveInfoUseItemID;
            int ActiveInfoOtherType;
            string QuestionInfoCorrectAnswer;
            string[] QuestionInfoWrongAnswerList;
            int QuestionInfoNpcID;
            string QuestionInfoConversion;
            string TaskInfoDescriptionListA;
            string TaskInfoPurposeListA;
            string TaskInfoTraceListA;
            int AIInfoAIID;
            int AIInfoBattleResult;
            int AIInfoDeathPunish;
            int AIInfoTeamSteate;
            string AIInfoBattleLevel;
            int BattleInfoBattleMapType;
            int BattleInfoBattleZoneID;
            int BattleInfoDrop;
            int BattleInfoBattleTimes;
            int[] BattleInfoMonsterList;
            int BattleInfoMonsterNum;
            int BattleInfoDropItemID;
            int BattleInfoDropItemNum;
            int ScenarioInfoAnimationID;
            int ScenarioInfoBranchNpcID;
            string ScenarioInfoBranchNote;
            string[] ScenarioInfoBranchOptionList;
            string[] ScenarioInfoNpcConversationList;
            int[] ScenarioInfoNpcID;
            string[] ScenarioInfoFinishConversationList;
            int[] ScenarioInfoFinishNpcID;
        }
        
        public void GetFieldInfos()
        {
            Type type = typeof(MissionField);

            FieldInfo[] fileds = type.GetFields();

            foreach (FieldInfo f in fileds)

            {

                Console.WriteLine(f.Name);//id name

            }
        }
    }
}
