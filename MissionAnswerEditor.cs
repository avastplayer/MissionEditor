using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionAnswerEditor
    {
        [DisplayName("��ȷ��")]
        [PropertyOrder(1)]
        public string QuestionInfoCorrectAnswer { get; set; }

        [DisplayName("����ѡ��1")]
        [PropertyOrder(2)]
        public string QuestionInfoWrongAnswerList0 { get; set; }

        [DisplayName("����ѡ��2")]
        [PropertyOrder(3)]
        public string QuestionInfoWrongAnswerList1 { get; set; }

        [DisplayName("����ѡ��3")]
        [PropertyOrder(4)]
        public string QuestionInfoWrongAnswerList2 { get; set; }

        [DisplayName("����ѡ��4")]
        [PropertyOrder(5)]
        public string QuestionInfoWrongAnswerList3 { get; set; }

        [DisplayName("����ѡ��5")]
        [PropertyOrder(6)]
        public string QuestionInfoWrongAnswerList4 { get; set; }

        [DisplayName("�Ի�NPC")]
        [PropertyOrder(7)]
        public int QuestionInfoConversion { get; set; }
    }
}