﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ActivacionEmail: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label3.Text = "Tu email es " + Request.QueryString["emailadd"].ToString() + " .Por favor comprueba en tu bandeja del " +
            "email para encontrar el código de activación.";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String miCon = "Data Source=LAPTOP-OGDFKH4G; Initial Catalog=tour2visitors; Integrated Security=True";
        String miConsulta = "select * from Usuario where correo='" + Request.QueryString["emailadd"] + "'";
        SqlConnection con = new SqlConnection(miCon);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = miConsulta;
        cmd.Connection = con;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        DataRow fila;
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            fila = ds.Tables[0].Rows[0];
            if (fila["codigoActivacion"].ToString().Trim() != TextBox1.Text)
            {
                cambiaEstado();
                Label4.Text = "Tu cuenta ha sido verificada correctamente";
            }
            else
            {
                Label4.Text = "Has introducido un código de verificación erróneo. Revisa tu bandeja del correo.";
            }
        }
        con.Close();
    }
    private void cambiaEstado()
    {
        String miCon = "Data Source=LAPTOP-OGDFKH4G; Initial Catalog=tour2visitors; Integrated Security=True";
        String actualizarDato = "Update Usuario set estadoConfEmail='Verif' where correo='" + Request.QueryString["emailadd"] + "'";
        SqlConnection con = new SqlConnection(miCon);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = actualizarDato;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();

    }
}