package leandrodotta.voz;

public interface TextToSpeechListener {
    void onInit();
    void onError();

    void onSpeechStart(String utteranceId);
    void onSpeechDone(String utteranceId);
    void onSpeechStop(String utteranceId);
    void onSpeechError(String utteranceId, int errorCode);
}
