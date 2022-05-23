package leandrodotta.voz;

import android.content.Context;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.speech.tts.Voice;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Locale;
import java.util.UUID;

public class VozTextToSpeech extends UtteranceProgressListener implements TextToSpeech.OnInitListener {
    private TextToSpeech tts;
    private TextToSpeechListener listener;

    private float pitch;
    private float speechRate;
    private boolean available;

    public VozTextToSpeech(final Context context, TextToSpeechListener listener) {
        tts = new TextToSpeech(context, this);
        this.listener = listener;
    }

    @Override
    public void onInit(int status) {
        if (status == TextToSpeech.ERROR) {
            if (listener != null)
                listener.onError();

            return;
        }

        tts.setLanguage(Locale.getDefault());
        tts.setOnUtteranceProgressListener(this);
        available = true;

        if (listener != null)
            listener.onInit();
    }

    public void speak(String textToSpeak){
        speak(textToSpeak, null, false);
    }

    public void speak(String textToSpeak, String utteranceId) {
        speak(textToSpeak, utteranceId, false);
    }

    public void speakImmediately(String textToSpeak){
        speak(textToSpeak, null, true);
    }

    public void speakImmediately(String textToSpeak, String utteranceId){
        speak(textToSpeak, utteranceId, true);
    }

    private void speak(String textToSpeak, String utteranceId, boolean flush) {
        if (utteranceId == null || utteranceId.length() == 0)
            utteranceId = UUID.randomUUID().toString();

        tts.speak(textToSpeak, flush ? TextToSpeech.QUEUE_FLUSH : TextToSpeech.QUEUE_ADD, null, utteranceId);
    }

    private void stop() {
        tts.stop();
    }

    public boolean isSpeaking() {
        return tts.isSpeaking();
    }

    public boolean isAvailable() {
        return available;
    }

    public float getPitch(){
        return pitch;
    }

    public float getSpeechRate(){
        return speechRate;
    }

    public boolean setPitch(float pitch){
        int result = tts.setPitch(pitch);

        if(result == TextToSpeech.SUCCESS){
            this.pitch = pitch;
            return true;
        }

        return false;
    }

    public boolean setSpeechRate(float speechRate){
        int result = tts.setSpeechRate(speechRate);

        if(result == TextToSpeech.SUCCESS){
            this.speechRate = speechRate;
            return true;
        }

        return false;
    }

    public String getCurrentVoice(){
        return tts.getVoice().getName();
    }

    public String[] getVoices(){
        List<String> voices = new ArrayList<>();

        String locale = tts.getVoice().getLocale().toString();

        for(Voice voice : tts.getVoices()){
            // Select only the voices of the current Locale and that doesn't need network connection
            if(voice.getLocale().toString().equals(locale)
                    && !voice.isNetworkConnectionRequired()
                    && voice.getName().endsWith("local")){

                voices.add(voice.getName());
            }
        }

        Collections.sort(voices);

        String[] voiceArray = new String[voices.size()];
        voices.toArray(voiceArray);
        return voiceArray;
    }

    public boolean setVoice(String voiceName){
        for(Voice voice : tts.getVoices()){
            if(voice.getName().equals(voiceName)){
                int result = tts.setVoice(voice);

                return result == TextToSpeech.SUCCESS;
            }
        }

        return false;
    }

    public void shutdown(){
        tts.shutdown();
    }

    @Override
    public void onStart(String utteranceId) {
        if (listener != null)
            listener.onSpeechStart(utteranceId);
    }

    @Override
    public void onDone(String utteranceId) {
        if (listener != null)
            listener.onSpeechDone(utteranceId);
    }

    @Override
    public void onStop(String utteranceId, boolean interrupted) {
        super.onStop(utteranceId, interrupted);
        if (listener != null)
            listener.onSpeechStop(utteranceId);
    }

    @Override
    public void onError(String utteranceId, int errorCode) {
        super.onError(utteranceId, errorCode);
        if (listener != null)
            listener.onSpeechError(utteranceId, errorCode);
    }

    @Override
    public void onError(String utteranceId) {
        if (listener != null)
            listener.onSpeechError(utteranceId, -1);
    }
}