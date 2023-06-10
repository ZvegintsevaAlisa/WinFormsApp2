using System.Text;

namespace game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        Battle battle;
        ChangeState changeState;
        private void Form1_Load(object sender, EventArgs e)
        {
            ArmyCreator ac = new RightArmyCreator();
            Army rightarmy = ac.CreateArmy(menu.rightarmy, menu.rightprice);
            ac = new LeftArmyCreator();
            Army leftarmy = ac.CreateArmy(menu.leftarmy, menu.leftprice);
           StringBuilder sb = new StringBuilder();
            foreach (Unit i in leftarmy.unitlist)
            {
                sb.Append(i.hp.ToString());
            }
            File.WriteAllText("loo.txt", sb.ToString());
            battle = new Battle(leftarmy, rightarmy);
            changeState = new ChangeState(battle);
        }
        public void Visualize(int i)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            battle.Move();
            //textBox1.Text = battle.g;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeState changeState = new ChangeState(battle);
            changeState.Execute();
            // battle = changeState.battle;
        }
    }
}