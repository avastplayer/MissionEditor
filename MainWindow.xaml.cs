using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace MissionEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private int SelectDataRow { get; set; }
        private HashSet<int> WriteRows { get; } = new HashSet<int>();
        private readonly MissionLimitEditor _missionLimitEditor = new MissionLimitEditor();
        private readonly MissionTriggeredEditor _missionTriggeredEditor = new MissionTriggeredEditor();
        private readonly MissionResultEditor _missionResultEditor = new MissionResultEditor();

        public MainWindow()
        {
            InitializeComponent();
            LoadLayout();
            SetDataGridItemSource();
            MissionLimitPropertyGrid.SelectedObject = _missionLimitEditor;
            MissionTriggeredPropertyGrid.SelectedObject = _missionTriggeredEditor;
            MissionResultPropertyGrid.SelectedObject = _missionResultEditor;
        }

        private void SetDataGridItemSource()
        {
            MissionDataGrid.ItemsSource = DatatableManager.Instance.Datatable.DefaultView;
        }

        private void SetMissionLimitPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MinLevel"].ToString(),
                out int minLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MaxLevel"].ToString(),
                out int maxLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransMinLevel"].ToString(),
                out int transMinLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransMaxLevel"].ToString(),
                out int transMaxLevel);

            _missionLimitEditor.MinLevel = minLevel;
            _missionLimitEditor.MaxLevel = maxLevel;
            _missionLimitEditor.TransMinLevel = transMinLevel;
            _missionLimitEditor.TransMaxLevel = transMaxLevel;
            
            _missionLimitEditor.RequestMissionList = new List<int>();
            _missionLimitEditor.RequestRoleIDList = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RequestMissionList" + i].ToString(),
                                    out int requestMission))
                {
                    _missionLimitEditor.RequestMissionList.Add(requestMission);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RequestRoleIdList" + i].ToString(),
                    out int requestRoleId))
                {
                    _missionLimitEditor.RequestRoleIDList.Add(requestRoleId);
                }
                else
                {
                    break;
                }
            }
            MissionLimitPropertyGrid.Update();
        }

        private void SetMissionTriggeredPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MissionType"].ToString(),
                out int missionType);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoNpcID"].ToString(),
                out int activeInfoNpcID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMapID"].ToString(),
                out int activeInfoMapID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTeamState"].ToString(),
                out int activeInfoTeamState);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoLeftPos"].ToString(),
                out int activeInfoLeftPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTopPos"].ToString(),
                out int activeInfoTopPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoRightPos"].ToString(),
                out int activeInfoRightPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoBottomPos"].ToString(),
                out int activeInfoBottomPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTargetID"].ToString(),
                out int activeInfoTargetID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTargetNum"].ToString(),
                out int activeInfoTargetNum);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMiniStep"].ToString(),
                out int activeInfoMiniStep);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoStepProbability"].ToString(),
                out int activeInfoStepProbability);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMaxStep"].ToString(),
                out int activeInfoMaxStep);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTimeLimit"].ToString(),
                out int activeInfoTimeLimit);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoIsRestartTimer"].ToString(),
                out int activeInfoIsRestartTimer);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoGiveBackMoney"].ToString(),
                out int activeInfoGiveBackMoney);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoGiveBackPetID"].ToString(),
                out int activeInfoGiveBackPetID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoUseItemID"].ToString(),
                out int activeInfoUseItemID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoOtherType"].ToString(),
                out int activeInfoOtherType);

            _missionTriggeredEditor.MissionType = (MissionTriggeredEditor.Type)missionType;
            _missionTriggeredEditor.ActiveInfoNpcID = activeInfoNpcID;
            _missionTriggeredEditor.ActiveInfoMapID = activeInfoMapID;
            _missionTriggeredEditor.ActiveInfoTeamState = (MissionTriggeredEditor.TeamState)activeInfoTeamState;
            _missionTriggeredEditor.ActiveInfoLeftPos = activeInfoLeftPos;
            _missionTriggeredEditor.ActiveInfoTopPos = activeInfoTopPos;
            _missionTriggeredEditor.ActiveInfoRightPos = activeInfoRightPos;
            _missionTriggeredEditor.ActiveInfoBottomPos = activeInfoBottomPos;
            _missionTriggeredEditor.ActiveInfoTargetID = activeInfoTargetID;
            _missionTriggeredEditor.ActiveInfoTargetNum = activeInfoTargetNum;
            _missionTriggeredEditor.ActiveInfoMiniStep = activeInfoMiniStep;
            _missionTriggeredEditor.ActiveInfoStepProbability = activeInfoStepProbability;
            _missionTriggeredEditor.ActiveInfoMaxStep = activeInfoMaxStep;
            _missionTriggeredEditor.ActiveInfoTimeLimit = activeInfoTimeLimit;
            _missionTriggeredEditor.ActiveInfoIsRestartTimer = activeInfoIsRestartTimer;
            _missionTriggeredEditor.ActiveInfoGiveBackMoney = activeInfoGiveBackMoney;
            _missionTriggeredEditor.ActiveInfoGiveBackPetID = activeInfoGiveBackPetID;
            _missionTriggeredEditor.ActiveInfoUseItemID = activeInfoUseItemID;
            _missionTriggeredEditor.ActiveInfoOtherType = activeInfoOtherType;
            
            MissionTriggeredPropertyGrid.Update();
        }

        private void SetMissionResultPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransformID"].ToString(),
                out int transformID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ExpReward"].ToString(),
                out int expReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MoneyReward"].ToString(),
                out int moneyReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["PetExpReward"].ToString(),
                out int petExpReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["SMoney"].ToString(),
                out int sMoney);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ShengWang"].ToString(),
                out int shengWang);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapJumpType"].ToString(),
                out int rewardMapJumpType);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBarTime"].ToString(),
                out int processBarTime);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapID"].ToString(),
                out int rewardMapID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBar"].ToString(),
                out int processBar);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapXPos"].ToString(),
                out int rewardMapXPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBarColor"].ToString(),
                out int processBarColor);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapYPos"].ToString(),
                out int rewardMapYPos);

            _missionResultEditor.PostMission = new int[50];
            _missionResultEditor.DisPlayNPCID = new int[50];
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["PostMission" + i].ToString(),
                    out int postMission))
                {
                    _missionResultEditor.PostMission[i] = postMission;
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["DisPlayNPCID" + i].ToString(),
                    out int disPlayNPCID))
                {
                    _missionResultEditor.DisPlayNPCID[i] = disPlayNPCID;
                }
                else
                {
                    break;
                }
            }
            _missionResultEditor.TransformID = transformID;
            _missionResultEditor.NoteInfo = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["NoteInfo"].ToString();
            _missionResultEditor.ExpReward = expReward;
            _missionResultEditor.MoneyReward = moneyReward;
            _missionResultEditor.PetExpReward = petExpReward;
            _missionResultEditor.SMoney = sMoney;
            _missionResultEditor.ShengWang = shengWang;
            _missionResultEditor.RewardMapJumpType = (MissionResultEditor.JumpType)rewardMapJumpType;
            _missionResultEditor.ProcessBarTime = processBarTime;
            _missionResultEditor.RewardMapID = rewardMapID;
            _missionResultEditor.ProcessBar = processBar;
            _missionResultEditor.RewardMapXPos = rewardMapXPos;
            _missionResultEditor.ProcessBarColor = processBarColor;
            _missionResultEditor.RewardMapYPos = rewardMapYPos;

            MissionResultPropertyGrid.Update();
        }

        private void MissionDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDataRow = GetSelectedRow();
            WriteRows.Add(SelectDataRow);
            SetMissionLimitPropertyGrid();
            SetMissionTriggeredPropertyGrid();
            SetMissionResultPropertyGrid();
        }

        private int GetSelectedRow()
        {
            if (MissionDataGrid != null && MissionDataGrid.SelectedCells.Count != 0)
            {
                return MissionDataGrid.SelectedIndex;
            }

            return -1;
        }

        private void DataGridSearchTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            SaveLayout();
        }

        private void SaveLayout()
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MissionDockingManager);
            using (var writer = new StreamWriter("MissionEditor.layout"))
            {
                layoutSerializer.Serialize(writer);
            }
        }

        private void LoadLayout()
        {
            if (!File.Exists("MissionEditor.layout")) return;
            
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MissionDockingManager);
            using (var reader = new StreamReader("MissionEditor.layout"))
            {
                layoutSerializer.Deserialize(reader);
            }
        }
    }
}