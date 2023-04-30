# 10 Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "1. Apply deployement"
cd C:\D\msc_project\msc-project\experiments
kubectl apply -f .\deployment.yml

write-host "2. Apply service"
kubectl apply -f .\service.yml

write-host "3. Apply service monitor"
kubectl apply -f .\servicemonitor.yml

write-host "4. Sleep 20 seconds to enable deployment to be up and running"
Start-Sleep -Seconds 20

write-host "5. Deploy horizontal scaler"
cd C:\D\msc_project\msc-project\experiments
kubectl apply -f .\hpa.yaml

write-host "6. Sleep 20 seconds to enable hpa to sync with metrics server"
Start-Sleep -Seconds 20