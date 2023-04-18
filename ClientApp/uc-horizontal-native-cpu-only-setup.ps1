# 10 Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "Deploy horizontal scaler"
cd C:\D\msc_project\msc-project\sample-app
kubectl apply -f .\hpa.yaml