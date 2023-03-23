package com.uaal.nativeandroid;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.unity3d.player.UnityPlayerActivity;

public class UnityHandlerActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_unity_handler);

        Button button = findViewById(R.id.btnClick);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(UnityHandlerActivity.this, UnityPlayerActivity.class);
                intent.putExtra("my_text", "First Intent");
                intent.putExtra("test", "Second Intent");
                if(intent != null){
                    startActivity(intent);
                }else{
                    Log.d("Unity", "Couldnt start unity game");
                }
            }
        });

        TextView receiver = findViewById(R.id.received_value_id);
        Intent intent = getIntent();
        String str = intent.getStringExtra("message_key");
        receiver.setText(str);
    }

    public static void Launch(String value)
    {
        Log.d("Unity", "Inside Launch " + value);
    }
}