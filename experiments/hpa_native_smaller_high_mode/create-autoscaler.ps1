# Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/


write-host "1. Moved control to correct folder"
cd C:\D\msc_project\msc-project\experiments\hpa_native_smaller_high_mode

write-host "2. Applying horizontal scaler"
kubectl apply -f hpa.yml

write-host "8. Finished applying autoscaler"