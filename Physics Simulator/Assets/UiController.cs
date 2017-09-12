using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiController : MonoBehaviour {

    #region Variables
    public static UiController instances;

    public GameObject Dimention_x, Dimention_y, Dimention_z;
    public Dropdown DropBoxDimention;
    public Dropdown DropBoxParticle;
    public Dropdown DropBoxCameraTarget;

    //Displacement inputs
    public InputField InputField_s_x, InputField_s_y, InputField_s_z;
    //Initial Velocity inputs
    public InputField InputField_u_x, InputField_u_y, InputField_u_z;
    //Final Velocity inputs
    public InputField InputField_v_x, InputField_v_y, InputField_v_z;
    //Acceleration inputs
    public InputField InputField_a_x, InputField_a_y, InputField_a_z;
    //Initial position inputs
    public InputField InputField_r_x, InputField_r_y, InputField_r_z;

    //Time input
    public InputField InputField_Time;
    //Radius input
    public InputField InputField_Radius;

    //Restitution inputs
    public Text LabelRestitution;
    public Slider SliderRestitution;

    //Mass input
    public InputField InputField_Mass;
    //Radius input

    //Simulation speed inputs
    public Text LabelSimulationSpeed;
    public Slider SliderSimulationSpeed;
    #endregion

    //Eventualy add a centre of mass location?
    private void UpdateCameraTarget()
    {
        int size = DropBoxParticle.options.Count;
        Dropdown.OptionData[] oldOptions = new Dropdown.OptionData[size + 1];
        for (int i = 0; i < size; i++)
        {
            oldOptions[i] = DropBoxParticle.options[i];
        }
        DropBoxCameraTarget.options.Clear();
        DropBoxCameraTarget.options.Add(new Dropdown.OptionData() { text = "Free Roam" });
        for (int i = 0; i < size - 1; i++)
        {
            DropBoxCameraTarget.options.Add(oldOptions[i]);
        }
    }

    public void OnCalculateClicked()
    {
        //prints to console
        Debug.Log("Button Clicked");
        CalculateController.CalculateControl();
    }

    public void OnSimulateClicked()
    {
        Particle values = CalculateController.CalculateControl();
        int dimentions = DropBoxDimention.value + 1;
        if (dimentions <= 2)
        {
            SimulateController.OnSimulateClicked();
        }
        else
        {
            //For when I want diffrent camera angles
            SimulateController.OnSimulateClicked();
        }
    }
    public void OnDropBoxParticleChanged()
    {
        int size = DropBoxParticle.options.Count;
        //Adding another options
        if (DropBoxParticle.value == size -1 && SimulateController.isSimulating == false)
        {
            Dropdown.OptionData[] oldOptions = new Dropdown.OptionData[size+1];
            for (int i =0;i<size;i++)
            {
                oldOptions[i] = DropBoxParticle.options[i];
            }
            DropBoxParticle.options.Clear();
            for (int i =0;i<size-1;i++)
            {
                DropBoxParticle.options.Add(oldOptions[i]);
            }
            string _text = "Particle " + size.ToString();
            DropBoxParticle.options.Add(new Dropdown.OptionData() { text = _text });

            _text = "Add Particle";
            DropBoxParticle.options.Add(new Dropdown.OptionData() { text = _text });

            DropBoxParticle.value = size-2;
            UpdateCameraTarget();
        }
        int current = DropBoxParticle.value;
        try
        {
            UpdateUI(Particle.Instances[current]);
        }
        catch(System.ArgumentOutOfRangeException e)
        {
            ResetUi();
        }


    }


    public void Start()
    {
        instances = this;
    }

    #region Sliders

    public static void UpdateSliderLabelCombo(Slider slid, Text label)
    {
        label.text = slid.value.ToString();
    }
    public void OnRestritutionSliderChanged()
    {
        UpdateSliderLabelCombo(SliderRestitution, LabelRestitution);
    }
    public void OnSimulationSliderChanged()
    {
        LabelSimulationSpeed.text = SliderSimulationSpeed.value.ToString() + ".0x";
    }

    #endregion

    public void OnDropBoxDimentionChanged()
    {
        switch (DropBoxDimention.value)
        {
            case 0:
                Dimention_x.SetActive(true);
                Dimention_y.SetActive(false);
                Dimention_z.SetActive(false);
                break;
            case 1:
                Dimention_x.SetActive(true);
                Dimention_y.SetActive(true);
                Dimention_z.SetActive(false);
                break;
            case 2:
                Dimention_x.SetActive(true);
                Dimention_y.SetActive(true);
                Dimention_z.SetActive(true);
                break;
            default:
                Dimention_x.SetActive(true);
                Dimention_y.SetActive(true);
                Dimention_z.SetActive(true);
                break;

        }

    }

    #region Ui updates and reset

    public void UpdateUI(Particle values)
    {
        InputField_s_x.text = values.Displacement[0].ToString();
        InputField_s_y.text = values.Displacement[1].ToString();
        InputField_s_z.text = values.Displacement[2].ToString();

        InputField_u_x.text = values.InitialVelociy[0].ToString();
        InputField_u_y.text = values.InitialVelociy[1].ToString();
        InputField_u_z.text = values.InitialVelociy[2].ToString();

        InputField_v_x.text = values.FinalVelocity[0].ToString();
        InputField_v_y.text = values.FinalVelocity[1].ToString();
        InputField_v_z.text = values.FinalVelocity[2].ToString();

        InputField_a_x.text = values.Acceleration[0].ToString();
        InputField_a_y.text = values.Acceleration[1].ToString();
        InputField_a_z.text = values.Acceleration[2].ToString();

        InputField_Time.text = values.Time.ToString();

        InputField_r_x.text = values.Position[0].ToString();
        InputField_r_y.text = values.Position[1].ToString();
        InputField_r_z.text = values.Position[2].ToString();

        InputField_Mass.text = values.Mass.ToString();
        InputField_Radius.text = values.Radius.ToString();
        SliderRestitution.value = values.Restitution;

    }

    public void ResetUi()
    {
        SimulateController.DestroyObjectsWithTag("Simulation");

        InputField_s_x.text = "";
        InputField_s_y.text = "";
        InputField_s_z.text = "";

        InputField_u_x.text = "";
        InputField_u_y.text = "";
        InputField_u_z.text = "";

        InputField_v_x.text = "";
        InputField_v_y.text = "";
        InputField_v_z.text = "";

        InputField_a_x.text = "";
        InputField_a_y.text = "";
        InputField_a_z.text = "";

        InputField_Time.text = "";

        InputField_r_x.text = "";
        InputField_r_y.text = "";
        InputField_r_z.text = "";

        InputField_Mass.text = "";
        InputField_Radius.text = "";
        SliderRestitution.value = 0;


    }
    #endregion

}
