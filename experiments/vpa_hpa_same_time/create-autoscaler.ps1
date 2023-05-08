# Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/


write-host "1. Moved control to correct folder"
cd C:\D\msc_project\msc-project\experiments\vpa_hpa_same_time

write-host "2. Applying horizontal scaler"
kubectl apply -f hpa.yml

write-host "3. Applying vertical scaler"
kubectl apply -f vpa.yml

write-host "4. Finished applying autoscalers"