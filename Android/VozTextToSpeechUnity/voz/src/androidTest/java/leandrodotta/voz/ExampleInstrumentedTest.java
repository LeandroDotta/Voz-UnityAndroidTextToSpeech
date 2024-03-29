package leandrodotta.voz;

import android.content.Context;

import androidx.test.platform.app.InstrumentationRegistry;
import androidx.test.ext.junit.runners.AndroidJUnit4;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.*;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * Instrumented test, which will execute on an Android device.
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
@RunWith(AndroidJUnit4.class)
public class ExampleInstrumentedTest {
    @Test
    public void useAppContext() {
        // Context of the app under test.
        Context appContext = InstrumentationRegistry.getInstrumentation().getTargetContext();
        assertEquals("leandrodotta.voz", appContext.getPackageName());
    }

    @Test
    public void speak() throws ExecutionException, InterruptedException {
        Context appContext = InstrumentationRegistry.getInstrumentation().getTargetContext();
        new TestSpeak().test(appContext);
    }

    private class TestSpeak implements TextToSpeechListener {
        private boolean speakDone = false;

        public void test(Context context) throws ExecutionException, InterruptedException {
            VozTextToSpeech voz = new VozTextToSpeech(context, this);

            ExecutorService executor = Executors.newSingleThreadExecutor();
            executor.submit(() -> {
                while (!voz.isAvailable()) {
                    try {
                        Thread.sleep(100);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }).get();

            voz.speak("Hello World");

            executor.submit(() -> {
                while (!speakDone)
                    try {
                        Thread.sleep(100);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
            }).get();

            Assert.assertTrue(true);
        }

        @Override
        public void onInit() {}

        @Override
        public void onError() {}

        @Override
        public void onSpeechStart(String utteranceId) {}

        @Override
        public void onSpeechDone(String utteranceId) {
            speakDone = true;
        }

        @Override
        public void onSpeechStop(String utteranceId) {}

        @Override
        public void onSpeechError(String utteranceId, int errorCode) {}
    }
}