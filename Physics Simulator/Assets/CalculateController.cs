using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateController : MonoBehaviour {

    public static Particle CalculateControl()
    {
        Particle values = new Particle();
        int dimentions = UiController.instances.DropBoxDimention.value + 1;
        //Getting Suvatr inputs in all dimentions
        #region Suvat Inputs
        if (dimentions >= 1)
        {
            GetInput_Suvat_x(ref values);
            if (dimentions >= 2)
            {
                GetInput_Suvat_y(ref values);
                if (dimentions >= 3)
                {
                    GetInput_Suvat_z(ref values);
                }
                else
                {
                    values.inValidInput[2] = true;
                }
            }
            else
            {
                values.inValidInput[1] = true;
                values.inValidInput[2] = true;
            }
        }
        else
        {
            values.inValidInput[0] = true;
        }
        #endregion

        //Get misc values
        GetInput_Misc(ref values);
        Particle finsihed = ValidationCheck(ref values);
        try
        {
            Particle.Instances[UiController.instances.DropBoxParticle.value] = finsihed;
        }
        catch (System.ArgumentOutOfRangeException e)
        {
            Particle.Instances.Add(finsihed);
        }

        return finsihed;
    }
    private static Particle ValidationCheck(ref Particle values)
    {
        //If one of the axis has more than or equal to 3 inputs (then the program can calculate)
        if (values.GetNumberOfInputs()[0] >= 3 || values.GetNumberOfInputs()[1] >= 3 || values.GetNumberOfInputs()[2] >= 3)
        {
            //Returns values with all values calculated
            values = SuvatSolvers.FindEquation(values);
            UiController.instances.UpdateUI(values);
            //return;
        }
        else
        {
            string msg = "You must input atleast 3 quantities in one of the dimentions";
            MessageBox(msg);
        }
        return values;
    }

    #region Get Inputs
    private static void GetInput_Suvat_x(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[0] = "";

        #region endless if statements
        if (Ui.InputField_s_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Displacement[0] = float.Parse(Ui.InputField_s_x.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_u_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.InitialVelociy[0] = float.Parse(Ui.InputField_u_x.text);
            values.Key[0] += "1";
        }

        if (Ui.InputField_v_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.FinalVelocity[0] = float.Parse(Ui.InputField_v_x.text);
            values.Key[0] += "1";
        }

        if (Ui.InputField_a_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Acceleration[0] = float.Parse(Ui.InputField_a_x.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_r_x.text != "")
        {
            values.Position[0] = float.Parse(Ui.InputField_r_x.text);
        }
        else
        {
            values.Position[0] = 0;
        }
        #endregion
    }

    private static void GetInput_Suvat_y(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[1] = "";

        #region endless if statements
        if (Ui.InputField_s_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Displacement[1] = float.Parse(Ui.InputField_s_y.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_u_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.InitialVelociy[1] = float.Parse(Ui.InputField_u_y.text);
            values.Key[1] += "1";
        }

        if (Ui.InputField_v_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.FinalVelocity[1] = float.Parse(Ui.InputField_v_y.text);
            values.Key[1] += "1";
        }

        if (Ui.InputField_a_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Acceleration[1] = float.Parse(Ui.InputField_a_y.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_r_y.text != "")
        {
            values.Position[1] = float.Parse(Ui.InputField_r_y.text);
        }
        else
        {
            values.Position[1] = 0;
        }
        #endregion
    }

    private static void GetInput_Suvat_z(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[2] = "";

        #region endless if statements
        if (Ui.InputField_s_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Displacement[2] = float.Parse(Ui.InputField_s_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_u_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.InitialVelociy[2] = float.Parse(Ui.InputField_u_z.text);
            values.Key[2] += "1";
        }

        if (Ui.InputField_v_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.FinalVelocity[2] = float.Parse(Ui.InputField_v_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_a_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Acceleration[2] = float.Parse(Ui.InputField_a_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_r_z.text != "")
        {
            values.Position[2] = float.Parse(Ui.InputField_r_z.text);
        }
        else
        {
            values.Position[2] = 0;
        }
        #endregion
    }


    private static void GetInput_Misc(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        if (Ui.InputField_Mass.text != "" && Ui.InputField_Mass.text != "0")
        {
            values.Mass = float.Parse(Ui.InputField_Mass.text);
        }
        else
        {
            values.Mass = 1;
        }
        values.Restitution = Ui.SliderRestitution.value;
        if (Ui.InputField_Radius.text != "" && Ui.InputField_Radius.text != "0")
        {
            values.Radius = float.Parse(Ui.InputField_Radius.text);
        }
        else
        {
            values.Radius = 1;
        }

        values.HasGravity = UiController.instances.ToggleGravity.isOn;
        values.canCollide = UiController.instances.ToggleCollisions.isOn;
    }

    #endregion


    private static void MessageBox(string msg)
    {
        Debug.Log(msg);
        throw new System.NotImplementedException();
    }


}
