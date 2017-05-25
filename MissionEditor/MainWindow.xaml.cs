using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MissionEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public int SelectDataRow { get; set; }
        public AssetManager AssetManager = new AssetManager();
        public HashSet<int> WriteRows { get; set; } = new HashSet<int>();

        public MainWindow()
        {
            DataProcess dataProcess = new DataProcess();
            dataProcess.GetFieldInfos();
            InitializeComponent();

            MissionDataGrid.ItemsSource = AssetManager.MissionDatatable.DefaultView;
        }

        private void MissionDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDataRow = GetSelectedRow();
            WriteRows.Add(SelectDataRow);
            BindingValue();
        }

        private void BindingValue()
        {
            BindingTextBoxValue(MinLevelTextBox, "[MinLevel]");
            BindingTextBoxValue(MaxLevelTextBox, "[MaxLevel]");
            BindingTextBoxValue(TransMinLevelTextBox, "[TransMinLevel]");
            BindingTextBoxValue(TransMaxLevelTextBox, "[TransMaxLevel]");

            BindingListBoxValue(RequestMissionListBox, "RequestMissionList");
            BindingListBoxValue(RequestRoleIDListBox, "RequestRoleIDList");
            BindingListBoxValue(PostMissionListBox, "PostMissionList");

            BindingTextBoxValue(NoteInfoTextBox, "[NoteInfo]");
            BindingTextBoxValue(ExpRewardTextBox, "[ExpReward]");
            BindingTextBoxValue(MoneyRewardTextBox, "[MoneyReward]");
            BindingTextBoxValue(PetExpRewardTextBox, "[PetExpReward]");
            BindingTextBoxValue(ShengWangTextBox, "[ShengWang]");
            BindingTextBoxValue(SMoneyTextBox, "[SMoney]");

            RewardMapJumpTypeComboBox.SelectedIndex = DataProcess.GetRewardMapJumpTypeSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["RewardMapJumpType"]));

            BindingTextBoxValue(RewardMapIDTextBox, "[RewardMapID]");
            BindingTextBoxValue(RewardMapXPosTextBox, "[RewardMapXPos]");
            BindingTextBoxValue(RewardMapYPosTextBox, "[RewardMapYPos]");
            BindingTextBoxValue(ProcessBarTimeTextBox, "[ProcessBarTime]");
            BindingTextBoxValue(ProcessBarTextTextBox, "[ProcessBarText]");
            BindingTextBoxValue(ProcessBarColorTextBox, "[ProcessBarColor]");

            BindingListBoxValue(DisPlayNPCIDListBox, "DisPlayNPCID");

            SetAwardItemCell();
            MissionTypeComboBox.SelectedIndex = DataProcess.GetMissionTypeSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["MissionType"]));

            BindingTextBoxValue(ActiveInfoNpcIDTextBox, "[ActiveInfoNpcID]");
            BindingTextBoxValue(ActiveInfoMapIDTextBox, "[ActiveInfoMapID]");
            BindingTextBoxValue(ActiveInfoLeftPosTextBox, "[ActiveInfoLeftPos]");
            BindingTextBoxValue(ActiveInfoTopPosTextBox, "[ActiveInfoTopPos]");
            BindingTextBoxValue(ActiveInfoRightPosTextBox, "[ActiveInfoRightPos]");
            BindingTextBoxValue(ActiveInfoBottomPosTextBox, "[ActiveInfoBottomPos]");
            BindingTextBoxValue(ActiveInfoTargetIDTextBox, "[ActiveInfoTargetID]");
            BindingTextBoxValue(ActiveInfoTargetNumTextBox, "[ActiveInfoTargetNum]");
            BindingTextBoxValue(ActiveInfoMiniStepTextBox, "[ActiveInfoMiniStep]");
            BindingTextBoxValue(ActiveInfoStepProbabilityTextBox, "[ActiveInfoStepProbability]");
            BindingTextBoxValue(ActiveInfoMaxStepTextBox, "[ActiveInfoMaxStep]");

            ActiveInfoTeamStateComboBox.SelectedIndex = DataProcess.GetActiveInfoTeamStateSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["ActiveInfoTeamState"]));

            BindingTextBoxValue(ActiveInfoTimeLimitTextBox, "[ActiveInfoTimeLimit]");
            BindingTextBoxValue(ActiveInfoIsRestartTimerTextBox, "[ActiveInfoIsRestartTimer]");
            BindingTextBoxValue(ActiveInfoGiveBackMoneyTextBox, "[ActiveInfoGiveBackMoney]");
            BindingTextBoxValue(ActiveInfoGiveBackPetIDTextBox, "[ActiveInfoGiveBackPetID]");
            BindingTextBoxValue(ActiveInfoUseItemIDTextBox, "[ActiveInfoUseItemID]");
            BindingTextBoxValue(ActiveInfoOtherTypeTextBox, "[ActiveInfoOtherType]");
            BindingTextBoxValue(QuestionInfoCorrectAnswerTextBox, "[QuestionInfoCorrectAnswer]");

            //QuestionInfoWrongAnswerList只绑定5个
            BindingTextBoxValue(QuestionInfoWrongAnswerList0, "[QuestionInfoWrongAnswerList0]");
            BindingTextBoxValue(QuestionInfoWrongAnswerList1, "[QuestionInfoWrongAnswerList1]");
            BindingTextBoxValue(QuestionInfoWrongAnswerList2, "[QuestionInfoWrongAnswerList2]");
            BindingTextBoxValue(QuestionInfoWrongAnswerList3, "[QuestionInfoWrongAnswerList3]");
            BindingTextBoxValue(QuestionInfoWrongAnswerList4, "[QuestionInfoWrongAnswerList4]");

            BindingTextBoxValue(QuestionInfoNpcIDTextBox, "[QuestionInfoNpcID]");
            BindingTextBoxValue(QuestionInfoConversionTextBox, "[QuestionInfoConversion]");
            BindingTextBoxValue(TaskInfoDescriptionListATextBox, "[TaskInfoDescriptionListA]");
            BindingTextBoxValue(TaskInfoPurposeListATextBox, "[TaskInfoPurposeListA]");
            BindingTextBoxValue(TaskInfoTraceListATextBox, "[TaskInfoTraceListA]");
            BindingTextBoxValue(AIInfoAIIDTextBox, "[AIInfoAIID]");

            //AIInfoBattleResult数据未绑定

            AIInfoDeathPunishComboBox.SelectedIndex = DataProcess.GetAIInfoDeathPunishSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["AIInfoDeathPunish"]));
            AIInfoTeamSteateComboBox.SelectedIndex = DataProcess.GetAIInfoTeamSteateSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["AIInfoTeamSteate"]));

            BindingTextBoxValue(AIInfoBattleLevelTextBox, "[AIInfoBattleLevel]");

            BattleInfoBattleMapTypeComboBox.SelectedIndex = DataProcess.GetBattleInfoBattleMapTypeSelectIndex(
                Convert.ToInt32(AssetManager.MissionDatatable.Rows[SelectDataRow]["BattleInfoBattleMapType"]));

            BindingTextBoxValue(BattleInfoBattleZoneIDTextBox, "[BattleInfoBattleZoneID]");
            BindingTextBoxValue(BattleInfoDropTextBox, "[BattleInfoDrop]");
            BindingTextBoxValue(BattleInfoBattleTimesTextBox, "[BattleInfoBattleTimes]");

            BindingListBoxValue(BattleInfoMonsterListBox, "BattleInfoMonsterList");

            BindingTextBoxValue(BattleInfoMonsterNumTextBox, "[BattleInfoMonsterNum]");
            BindingTextBoxValue(BattleInfoDropItemIDTextBox, "[BattleInfoDropItemID]");
            BindingTextBoxValue(BattleInfoDropItemNumTextBox, "[BattleInfoDropItemNum]");

            //BindingTextBoxValue(ScenarioInfoAnimationIDTextBox, "[ScenarioInfoAnimationID]");
            //BindingTextBoxValue(ScenarioInfoBranchNpcIDTextBox, "[ScenarioInfoBranchNpcID]");
            //BindingTextBoxValue(ScenarioInfoBranchNoteTextBox, "[ScenarioInfoBranchNote]");
            //TODO：ScenarioInfoBranchOptionList
            SetConversationCell();
            SetFinishConversationCell();
        }

        private int GetSelectedRow()
        {
            if (MissionDataGrid != null && MissionDataGrid.SelectedCells.Count != 0)
            {
                return MissionDataGrid.SelectedIndex;
            }

            return -1;
        }

        private void BindingTextBoxValue(FrameworkElement textBox, string value)
        {
            textBox.SetBinding(TextBox.TextProperty, new Binding
            {
                Source = AssetManager.MissionDatatable.Rows[SelectDataRow],
                Path = new PropertyPath(value),
                Mode = BindingMode.TwoWay
            });
        }

        private void BindingListBoxValue(ItemsControl listBox, string field, int number = 50)
        {
            listBox.Items.Clear();
            for (int i = 0; i < number; i++)
            {
                if (AssetManager.MissionDatatable.Rows[SelectDataRow][field + i].ToString() == "") break;
                listBox.Items.Add(AssetManager.MissionDatatable.
                    Rows[SelectDataRow][field + i].ToString());
            }
        }

        private void SetConversationCell()
        {
            ScenarioInfoNpcConversationListBox.Items.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(AssetManager.MissionDatatable.Rows[SelectDataRow]["ScenarioInfoNpcID" + i].ToString(), out int npcId))
                {
                    string conversation = AssetManager.MissionDatatable.Rows[SelectDataRow]["ScenarioInfoNpcConversationList" + i].ToString();
                    AssetManager.GetNpcInfo(npcId, out string npcName, out BitmapSource headBitmapSource);
                    ScenarioInfoNpcConversationListBox.Items.Add(new ConversationCell(npcName, headBitmapSource, conversation, ScenarioInfoNpcConversationListBox.ActualWidth - 5));
                }
            }
        }

        private void SetFinishConversationCell()
        {
            ScenarioInfoFinishConversationListBox.Items.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(AssetManager.MissionDatatable.Rows[SelectDataRow]["ScenarioInfoFinishNpcID" + i].ToString(), out int npcId))
                {
                    string conversation = AssetManager.MissionDatatable.Rows[SelectDataRow]["ScenarioInfoFinishConversationList" + i].ToString();
                    AssetManager.GetNpcInfo(npcId, out string npcName, out BitmapSource headBitmapSource);
                    ScenarioInfoFinishConversationListBox.Items.Add(new ConversationCell(npcName, headBitmapSource, conversation, ScenarioInfoFinishConversationListBox.ActualWidth - 5));
                }
            }
        }

        private void SetAwardItemCell()
        {
            RewardItemListbox.Items.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(AssetManager.MissionDatatable.Rows[SelectDataRow]["RewardItemIDList" + i].ToString(), out int itemId))
                {
                    string itemNum = AssetManager.MissionDatatable.Rows[SelectDataRow]["RewardItemNumList" + i].ToString();
                    string itemIsBind = AssetManager.MissionDatatable.Rows[SelectDataRow]["RewardItemIsBindList" + i].ToString();
                    AssetManager.GetItemInfo(itemId, out string itemName, out int itemIcon);
                    BitmapSource itemIconSource =
                        AssetManager.GetImage(AssetManager.ItemIconFileName, itemIcon.ToString());
                    RewardItemListbox.Items.Add(new AwardItemCell(itemId.ToString(), itemNum, itemName, itemIconSource, itemIsBind, RewardItemListbox.ActualWidth - 5));
                }
            }
        }

        private void MissionTypeComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow missionTypeDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            missionTypeDataRow.BeginEdit();
            missionTypeDataRow["MissionType"] = DataProcess.GetMissionType(MissionTypeComboBox.SelectedIndex);
            missionTypeDataRow.EndEdit();
        }

        private void ActiveInfoTeamStateComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow activeInfoTeamStateDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            activeInfoTeamStateDataRow.BeginEdit();
            activeInfoTeamStateDataRow["ActiveInfoTeamState"] = DataProcess.GetActiveInfoTeamState(ActiveInfoTeamStateComboBox.SelectedIndex);
            activeInfoTeamStateDataRow.EndEdit();
        }

        private void BattleInfoBattleMapTypeComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow battleInfoBattleMapTypeDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            battleInfoBattleMapTypeDataRow.BeginEdit();
            battleInfoBattleMapTypeDataRow["BattleInfoBattleMapType"] = DataProcess.GetBattleInfoBattleMapType(BattleInfoBattleMapTypeComboBox.SelectedIndex);
            battleInfoBattleMapTypeDataRow.EndEdit();
        }

        private void AIInfoTeamSteateComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow aiInfoTeamSteateDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            aiInfoTeamSteateDataRow.BeginEdit();
            aiInfoTeamSteateDataRow["AIInfoTeamSteate"] = DataProcess.GetAIInfoTeamSteate(AIInfoTeamSteateComboBox.SelectedIndex);
            aiInfoTeamSteateDataRow.EndEdit();
        }

        private void AIInfoDeathPunishComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow aiInfoDeathPunishDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            aiInfoDeathPunishDataRow.BeginEdit();
            aiInfoDeathPunishDataRow["AIInfoDeathPunish"] = DataProcess.GetAIInfoDeathPunish(AIInfoDeathPunishComboBox.SelectedIndex);
            aiInfoDeathPunishDataRow.EndEdit();
        }

        private void RewardMapJumpTypeComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            DataRow rewardMapJumpTypeDataRow = AssetManager.MissionDatatable.Rows[SelectDataRow];
            rewardMapJumpTypeDataRow.BeginEdit();
            rewardMapJumpTypeDataRow["RewardMapJumpType"] = DataProcess.GetRewardMapJumpType(RewardMapJumpTypeComboBox.SelectedIndex);
            rewardMapJumpTypeDataRow.EndEdit();
        }

        private void SaveButton_click(object sender, MouseButtonEventArgs e)
        {
            AssetManager.SaveExcel(WriteRows);
        }

        private void SaveAllButton_click(object sender, MouseButtonEventArgs e)
        {
            AssetManager.SaveExcel(WriteRows);
        }
    }
}